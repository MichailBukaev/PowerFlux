using PowerFlux.Services.Services.Computation.Models;
using PowerFlux.Services.Services.FluxCalculator.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PowerFlux.Services.Services.Computation
{
  public interface IComputationService
  {
    /// <param name="metalDepositedMass">The metal deposited mass, g</param>
    /// <param name="alloyingElementPercentInMetalDeposited">Alloying element concentration in deposited metal, %</param>
    /// <returns>Returns mass of alloying element in metal deposited, g</returns>
    double GetAlloyingElementMassInMetalDeposited(double metalDepositedMass, double alloyingElementPercentInMetalDeposited);

    Task<AlloyingElementPartialTransformationCoefficients> GetAlloyingElementPartialTransformationCoefficients(int alloyingElementId, EquationParameters argument);

    /// <param name="electrodeDiameter">Diameter of electrode, mm</param>
    /// <returns>Returns the cross sectional area of electrode, mm</returns>
    double GetElectrodeCrossSectionalArea(double electrodeDiameter);

    /// <param name="sourcePower">Total power of the heating source, kW</param>
    /// <returns>Returns the metal deposited mass, g</returns>
    Task<double> GetMetalDepositedMassAsync(double coatingMassCoefficient, double sourcePower);

    /// <param name="sourcePower">Total power of the heating source, kW</param>
    /// <returns>Returns coefficient of metal loss</returns>
    Task<double> GetMetalLossCoefficientAsync(double coatingMassCoefficient, double sourcePower);

    /// <param name="weldingCurrent">Welding current, A</param>
    /// <param name="arcVoltage">Arc voltage, V</param>
    /// <returns>Returns the total power of the heating source, kW.</returns>
    double GetSourcePower(double weldingCurrent, double arcVoltage);
  }
}
