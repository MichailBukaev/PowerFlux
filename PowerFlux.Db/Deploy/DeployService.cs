using Microsoft.Extensions.Logging;
using System;
using System.Data.Entity;
using System.Threading.Tasks;
using PowerFlux.Db.Entities;
using PowerFlux.Db.DbContexts.Interfaces;
using System.Collections.Generic;

namespace PowerFlux.Db.Deploy
{
  public class DeployService : IDeployService
  {
    private readonly ILogger<DeployService> _logger;
    private readonly IDbVersionContext _dbVersionsContext;
    private readonly IAlloyingElementContext _alloyingElementContext;
    private readonly ISettingsContext _settingsContext;
    private readonly IAlloyingElementPartialTransformationEquationContext _alloyingElementPartialTransformationEquationContext;

    public DeployService(ILogger<DeployService> logger,
      IDbVersionContext dbVersionsContext,
      IAlloyingElementContext alloyingElementContext,
      ISettingsContext settingsContext,
      IAlloyingElementPartialTransformationEquationContext alloyingElementPartialTransformationEquationContext)
    {
      _logger = logger;
      _dbVersionsContext = dbVersionsContext;
      _alloyingElementContext = alloyingElementContext;
      _settingsContext = settingsContext;
      _alloyingElementPartialTransformationEquationContext = alloyingElementPartialTransformationEquationContext;
    }

    public async Task<DbVersion> Load()
    {
      var dbVersion = await _dbVersionsContext.Entities.FirstOrDefaultAsync();
      if (dbVersion == null)
        return await LoadData();

      if (!dbVersion.IsFilledDb)
        return await LoadData();

      return dbVersion;
    }

    private async Task<DbVersion> LoadData()
    {
      using var transaction = _dbVersionsContext.GetAndBeginTransaction();
      try
      {
        _logger.LogInformation("Load data to DB starting");
        var version = await _dbVersionsContext.AddAsync(new DbVersion {Version = "1.0"});

        await LoadSettingsAsync();
        await LoadAlloyingElementsAsync();
        await LoadAlloyingElementPartialTransformationEquationAsync();

        version.IsFilledDb = true;
        await _dbVersionsContext.UpdateAsync(version);
        transaction.Commit();
        _logger.LogInformation("Load data to DB end successfully");
        return version;
      }
      catch (Exception ex)
      {
        transaction.Rollback();
        _logger.LogError("Load data to DB starting failed. Exception {@exception}", ex.Message);
        throw new Exception("Load data to DB starting failed", ex);
      }
    }

    private async Task LoadAlloyingElementPartialTransformationEquationAsync()
    {
      var equations = new List<AlloyingElementPartialTransformationEquation>();
      equations = await AddEquationAsync(equations, "Mn", toFerroalloyEquation: "1.30 - 2.15 * (P / S)", toKernelEquation: "1.83 - 4.32 * (P / S)", toGasEquation: "1.41 - 2.65 * (P / S)", toSlagEquation: "0");
      equations = await AddEquationAsync(equations, "Si", toFerroalloyEquation: "0.67 - 1.40 * (P / S)", toKernelEquation: "0.85 - 2.0 * (P / S)", toGasEquation: "0.85 - 2.0 * (P / S)", toSlagEquation: "0");
      equations = await AddEquationAsync(equations, "C", toFerroalloyEquation: "2.81 - 6.86 * (P / S)", toKernelEquation: "1.24 - 1.53 * (P / S)", toGasEquation: "1.20 - 1.59 * (P / S)", toSlagEquation: "0");
      equations = await AddEquationAsync(equations, "Ti", toFerroalloyEquation: "0", toKernelEquation: "0", toGasEquation: "(25.9 * (P / S) - 2.74) * 0,0001", toSlagEquation: "(25.9 * (P / S) - 2.74) * 0,0001");
      await _alloyingElementPartialTransformationEquationContext.AddRangeAsync(equations);
    }

    private async Task<List<AlloyingElementPartialTransformationEquation>> AddEquationAsync(List<AlloyingElementPartialTransformationEquation> equations, string alloyingElementsSymbol, string toFerroalloyEquation, string toKernelEquation,
      string toGasEquation, string toSlagEquation)
    {
      var alloyingElement = await _alloyingElementContext.Entities.FirstOrDefaultAsync(e => e.Symbol.Equals(alloyingElementsSymbol));
      if (alloyingElement == null)
        throw new Exception($"Alloyng element with symbol {alloyingElementsSymbol} not found");
      var equation = new AlloyingElementPartialTransformationEquation
      {
        AlloyingElement = alloyingElement,
        ToFerroalloyEquation = toFerroalloyEquation,
        ToGasEquation = toGasEquation,
        ToKernelEquation = toKernelEquation,
        ToSlagEquation = toSlagEquation
      };
      equations.Add(equation);
      return equations;
    }

    private Task LoadSettingsAsync()
    {
      var settings = new List<Settings>
      {
        new Settings {Key = "coatingMassCoefficient.max", Value = "0.45", DispleedName = "Max value of coating mass coefficient"},
        new Settings {Key = "coatingMassCoefficient.min", Value = "0.35", DispleedName = "Min value of coating mass coefficient"}
      };
      return _settingsContext.AddRangeAsync(settings);
    }

    private Task LoadAlloyingElementsAsync()
    {
      var alloyingElements = new List<AlloyingElement>
      {
        new AlloyingElement {Name = "Manganum", Symbol = "Mn"},
        new AlloyingElement {Name = "Silicium", Symbol = "Si"},
        new AlloyingElement {Name = "Carboneum", Symbol = "C"},
        new AlloyingElement {Name = "Titanium", Symbol = "Ti"}
      };
      return _alloyingElementContext.AddRangeAsync(alloyingElements);
    }
  }
}