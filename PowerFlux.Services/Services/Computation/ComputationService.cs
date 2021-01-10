using Microsoft.Extensions.Logging;
using PowerFlux.Db.DbContexts.Interfaces;
using PowerFlux.Services.Services.Computation.Models;
using PowerFlux.Services.Services.FluxCalculator.Models;
using PowerFlux.Services.Services.Settings;
using System;
using System.Data.Entity;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace PowerFlux.Services.Services.Computation
{
  public class ComputationService : IComputationService
  {
    private readonly ISettingsService _settingsService;
    private readonly IAlloyingElementPartialTransformationEquationContext _transformationEquationContext;
    private readonly ILogger<ComputationService> _logger;

    public ComputationService(
      ISettingsService settingsService, 
      IAlloyingElementPartialTransformationEquationContext transformationEquationContext,
      ILogger<ComputationService> logger)
    {
      _settingsService = settingsService;
      _transformationEquationContext = transformationEquationContext;
      _logger = logger;
    }

    public double GetAlloyingElementMassInMetalDeposited(double metalDepositedMass, double alloyingElementPercentInMetalDeposited)
    {
      if (metalDepositedMass == 0 || alloyingElementPercentInMetalDeposited == 0)
        return 0;

      if (metalDepositedMass < 0)
        throw new ArgumentException("Mass of metal deposited can't be less than 0");

      if (alloyingElementPercentInMetalDeposited < 0)
        throw new ArgumentException("Alloying element percent can't be less than 0");

      return metalDepositedMass * alloyingElementPercentInMetalDeposited / 100;
    }

    public async Task<AlloyingElementPartialTransformationCoefficients> GetAlloyingElementPartialTransformationCoefficients(int alloyingElementId, EquationParameters argument)
    {
      if (argument == null)
        throw new ArgumentNullException(nameof(argument));

      var result = new AlloyingElementPartialTransformationCoefficients(alloyingElementId);

      var equation = await _transformationEquationContext.Entities.FirstOrDefaultAsync(e => e.AlloyingElementId == alloyingElementId);
      if (equation == null)
        throw new ArgumentException($"For alloying element {alloyingElementId} not exists transformation equation");

      result.ToFerroalloy = ExecutEquation<EquationParameters, double>(equation.ToFerroalloyEquation, argument);
      result.ToGas = ExecutEquation<EquationParameters, double>(equation.ToGasEquation, argument);
      result.ToKernel = ExecutEquation<EquationParameters, double>(equation.ToKernelEquation, argument);
      result.ToSlag = ExecutEquation<EquationParameters, double>(equation.ToSlagEquation, argument);

      return result;
    }

    public double GetElectrodeCrossSectionalArea(double electrodeDiameter)
    {
      if (electrodeDiameter <= 0)
        throw new ArgumentException("Diameter of electrode should be more than 0");

      return Math.Pow(electrodeDiameter, 2) * 3.14;
    }

    public async Task<double> GetMetalDepositedMassAsync(double coatingMassCoefficient, double sourcePower)
    {
      if (sourcePower <= 0)
        throw new ArgumentException("Source power should be more than 0");

      await CheckCoatingMassCoefficient(coatingMassCoefficient);

      return (33.9 - (1.5 * sourcePower)) / coatingMassCoefficient;
    }

    public async Task<double> GetMetalLossCoefficientAsync(double coatingMassCoefficient, double sourcePower)
    {
      if (sourcePower <= 0)
        throw new ArgumentException("Source power should be more than 0");

      await CheckCoatingMassCoefficient(coatingMassCoefficient);

      return (1.3 * coatingMassCoefficient) + (0.025 * sourcePower) - 0.557;
    }

    public double GetSourcePower(double weldingCurrent, double arcVoltage) => (Math.Abs(weldingCurrent) * Math.Abs(arcVoltage)) / 1000;

    private async Task CheckCoatingMassCoefficient(double coatingMassCoefficient)
    {
      var coatingMassCoefficientMin = await _settingsService.GetValueAs<double>(Consts.SettingsNames.CoatingMassCoefficientMin, double.TryParse);
      var coatingMassCoefficientMax = await _settingsService.GetValueAs<double>(Consts.SettingsNames.CoatingMassCoefficientMax, double.TryParse);

      if (coatingMassCoefficient < coatingMassCoefficientMin || coatingMassCoefficient > coatingMassCoefficientMax)
        throw new ArgumentException("Coating mass coefficient shold be in 0.35 to 0.45 inclusive");
    }

    private TResult ExecutEquation<TArgument, TResult>(string equation, TArgument argument) where TArgument : class where TResult : struct
    {
      if (string.IsNullOrEmpty(equation))
        throw new ArgumentException("Equation cant be empty. If for alloying element partial transformation coefficient not exists, equation should be \"0\"");

      try
      {
        var toFerroalloyEquation = DynamicExpressionParser.ParseLambda(typeof(TArgument), typeof(TResult), equation).Compile();
        return (TResult)toFerroalloyEquation.DynamicInvoke(argument);
      }
      catch (Exception ex)
      {
        _logger.LogError("Can't parse and execut equation {@equation}. Error {@ex}", equation, ex);
        throw new Exception($"Equation [{equation}] is incorrect");
      }
    }
  }
}