using AutoFixture;
using AutoFixture.AutoMoq;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using PowerFlux.Db.DbContexts.Interfaces;
using PowerFlux.Db.Entities;
using PowerFlux.Services;
using PowerFlux.Services.Services.Computation;
using PowerFlux.Services.Services.Computation.Models;
using PowerFlux.Services.Services.FluxCalculator.Models;
using PowerFlux.Services.Services.Settings;
using PowerFlux.UnitTests.DbSetMoqing;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PowerFlux.UnitTests.Services.Computation
{
  [TestFixture]
  [Parallelizable]
  public class ComputationServiceTest
  {
    private IFixture _fixture;

    private Mock<ISettingsService> _settingsServiceMock;
    private Mock<IAlloyingElementPartialTransformationEquationContext> _transformationEquationMock;


    [SetUp]
    public void SetUp()
    {
      _fixture = new Fixture().Customize(new AutoMoqCustomization());

      _settingsServiceMock = _fixture.Freeze<Mock<ISettingsService>>();
      _transformationEquationMock = _fixture.Freeze<Mock<IAlloyingElementPartialTransformationEquationContext>>();
    }

    #region GetSourcePowerTests

    [TestCase(150, 24, 3.6)]
    [TestCase(-150, -24, 3.6)]
    [TestCase(150, -24, 3.6)]
    [TestCase(-150, 24, 3.6)]
    public void ShouldSuccessfullyGetSourcePowerTest(double weldingCurrent, double arcVoltage, double expectedResult)
    {
      //Given
      var sut = GetSut();
      //When
      var result = sut.GetSourcePower(weldingCurrent, arcVoltage);
      //Then
      result.Should().Be(expectedResult);
    }

    #endregion

    #region GetElectrodeCrossSectionalAreaTests

    [Test]
    public void ShouldSuccessfullyGetElectrodeCrossSectionalAreaTest()
    {
      //Given
      var electrodeDiameter = 2;
      var expectedResult = 12.56;

      var sut = GetSut();
      //When
      var result = sut.GetElectrodeCrossSectionalArea(electrodeDiameter);
      //Then
      result.Should().Be(expectedResult);
    }

    [TestCase(0)]
    [TestCase(-1)]
    public void ShouldFailedGetElectrodeCrossSectionalAreaWhenElectrodeDameterIsZeroOrLessTest(double electrodeDiameter)
    {
      //Given
      var sut = GetSut();
      //When
      Func<double> func = () => sut.GetElectrodeCrossSectionalArea(electrodeDiameter);
      //Then
      func.Should().Throw<ArgumentException>().WithMessage("Diameter of electrode should be more than 0");
    }

    #endregion

    #region GetMetalDepositedMassTests

    [Test]
    public async Task ShouldSuccessfullyGetMetalDepositedMassTest()
    {
      //Givan
      var coatingMassCoefficient = 0.38;
      var sourcePower = 3.6;
      var expectedResult = 75;

      _settingsServiceMock.Setup(m => m.GetValueAs<double>(Consts.SettingsNames.CoatingMassCoefficientMax, double.TryParse))
        .ReturnsAsync(0.45);

      _settingsServiceMock.Setup(m => m.GetValueAs<double>(Consts.SettingsNames.CoatingMassCoefficientMin, double.TryParse))
       .ReturnsAsync(0.35);

      var sut = GetSut();
      //When
      var result = await sut.GetMetalDepositedMassAsync(coatingMassCoefficient, sourcePower);
      //Then
      result.Should().Be(expectedResult);
    }

    [TestCase(0.34, 1, "Coating mass coefficient shold be in 0.35 to 0.45 inclusive")]
    [TestCase(0.46, 1, "Coating mass coefficient shold be in 0.35 to 0.45 inclusive")]
    [TestCase(0.40, 0, "Source power should be more than 0")]
    [TestCase(0.40, -1, "Source power should be more than 0")]
    public void ShouldFailedGetMetalDepositedMassWhenCoatingMassCoefficientHasIncorrectValueOrSoursePowerIsZerroOrLassValueTest(double coatingMassCoefficient, double sourcePower, string message)
    {
      //Given
      _settingsServiceMock.Setup(m => m.GetValueAs<double>(Consts.SettingsNames.CoatingMassCoefficientMax, double.TryParse))
        .ReturnsAsync(0.45);

      _settingsServiceMock.Setup(m => m.GetValueAs<double>(Consts.SettingsNames.CoatingMassCoefficientMin, double.TryParse))
       .ReturnsAsync(0.35);

      var sut = GetSut();
      //When
      Func<Task<double>> func = async () => await sut.GetMetalDepositedMassAsync(coatingMassCoefficient, sourcePower);
      //Then
      func.Should().Throw<ArgumentException>().WithMessage(message);
    }

    #endregion

    #region GetAlloyingElementMassInMetalDepositedTests

    [TestCase(75.0, 0.085, 0.06375000000000001)]
    [TestCase(75.0, 0.2, 0.15)]
    [TestCase(0, 0.085, 0)]
    [TestCase(75.0, 0, 0)]
    public void ShouldSuccessfullyGetAlloyingElementMassInMetalDepositedTest(double metalDepositedMass, double alloyingElementPercentInMetalDeposited, double expectedResult)
    {
      //Given
      var sut = GetSut();
      //When
      var result = sut.GetAlloyingElementMassInMetalDeposited(metalDepositedMass, alloyingElementPercentInMetalDeposited);
      //Then
      result.Should().Be(expectedResult);
    }

    [TestCase(-1, 0.085, "Mass of metal deposited can't be less than 0")]
    [TestCase(75.0, -1, "Alloying element percent can't be less than 0")]
    public void ShouldFailGetAlloyingElementMassInMetalDepositedWhenAnyArgumentHasValueLessThanZeroTest(double metalDepositedMass, double alloyingElementPercentInMetalDeposited, string message)
    {
      //Given
      var sut = GetSut();
      //When
      Func<double> func = () => sut.GetAlloyingElementMassInMetalDeposited(metalDepositedMass, alloyingElementPercentInMetalDeposited);
      //Then
      func.Should().Throw<ArgumentException>().WithMessage(message);
    }

    #endregion

    #region GetMetalLossCoefficientTests

    [TestCase(0.38, 3.6, 0.027000000000000024)]
    [TestCase(0.4, 2, 0.013000000000000012)]
    public async Task ShouldSuccessfullyGetMetalLossCoefficientTest(double coatingMassCoefficient, double sourcePower, double expectedresult)
    {
      //Given
      _settingsServiceMock.Setup(m => m.GetValueAs<double>(Consts.SettingsNames.CoatingMassCoefficientMax, double.TryParse))
       .ReturnsAsync(0.45);

      _settingsServiceMock.Setup(m => m.GetValueAs<double>(Consts.SettingsNames.CoatingMassCoefficientMin, double.TryParse))
       .ReturnsAsync(0.35);

      var sut = GetSut();
      //When
      var result = await sut.GetMetalLossCoefficientAsync(coatingMassCoefficient, sourcePower);
      //Then
      result.Should().Be(expectedresult);
    }

    [TestCase(0.34, 1, "Coating mass coefficient shold be in 0.35 to 0.45 inclusive")]
    [TestCase(0.46, 1, "Coating mass coefficient shold be in 0.35 to 0.45 inclusive")]
    [TestCase(0.40, 0, "Source power should be more than 0")]
    [TestCase(0.40, -1, "Source power should be more than 0")]
    public void ShouldFailGetMetalLossCoefficientWhenCoatingMassCoefficientHasIncorrectValueOrSoursePowerIsZerroOrLassValueTest(double coatingMassCoefficient, double sourcePower, string message)
    {
      //Given
      _settingsServiceMock.Setup(m => m.GetValueAs<double>(Consts.SettingsNames.CoatingMassCoefficientMax, double.TryParse))
        .ReturnsAsync(0.45);

      _settingsServiceMock.Setup(m => m.GetValueAs<double>(Consts.SettingsNames.CoatingMassCoefficientMin, double.TryParse))
       .ReturnsAsync(0.35);

      var sut = GetSut();
      //When
      Func<Task<double>> func = async () => await sut.GetMetalLossCoefficientAsync(coatingMassCoefficient, sourcePower);
      //Then
      func.Should().Throw<ArgumentException>().WithMessage(message);
    }

    #endregion

    #region  GetAlloyingElementPartialTransformationCoefficientsTests

    [TestCase(10, 0, 0.6837579617834396, 0.6504458598726114, 0.591783439490446)]
    [TestCase(20, 4.683566878980891, 0, 4.683566878980891, 0)]
    public async Task ShouldSuccessfullyGetAlloyingElementPartialTransformationCoefficientsTest(int alloyingElementId, double toSlag, double toFerroalloy, double toGas, double toKernel)
    {
      //Given
      var sourcePower = 3.6;
      var electrodeCrossSectionalArea = 12.56;
      var argument = new EquationParameters(sourcePower, electrodeCrossSectionalArea);

      var expectedResult = new AlloyingElementPartialTransformationCoefficients(alloyingElementId) { 
        ToFerroalloy = toFerroalloy,
        ToGas = toGas,
        ToKernel = toKernel,
        ToSlag = toSlag
      };

      _transformationEquationMock.SetupGet(p => p.Entities)
        .Returns(DbSetMockFactory.SetupDbSetMoq(new AlloyingElementPartialTransformationEquation[]
        {
          new AlloyingElementPartialTransformationEquation
          {Id = 1, AlloyingElementId = 10, ToFerroalloyEquation = "1.30 - 2.15 * (P / S)", ToKernelEquation = "1.83 - 4.32 * (P / S)", ToGasEquation = "1.41 - 2.65 * (P / S)", ToSlagEquation = "0"},

          new AlloyingElementPartialTransformationEquation
          {Id = 2, AlloyingElementId = 20, ToGasEquation = "(25.9 * (P / S) - 2.74) * 1", ToSlagEquation = "(25.9 * (P / S) - 2.74) * 1", ToFerroalloyEquation = "0", ToKernelEquation = "0"},

        }.AsQueryable())
       .Object);

      var sut = GetSut();
      //When
      var result = await sut.GetAlloyingElementPartialTransformationCoefficients(alloyingElementId, argument);
      //Then
      result.Should().BeEquivalentTo(expectedResult);
    }

    [Test]
    public void ShouldFailGetAlloyingElementPartialTransformationCoefficientsWhenArgumentIsNullTest()
    {
      //Given
      EquationParameters argument = null;

      var sut = GetSut();
      //When
      Func<Task<AlloyingElementPartialTransformationCoefficients>> func = async () => await sut.GetAlloyingElementPartialTransformationCoefficients(1, argument);
      //Then
      func.Should().Throw<ArgumentNullException>();
    }

    [Test]
    public void ShouldFailGetAlloyingElementPartialTransformationCoefficientsWhenEquationsIsNullTest()
    {
      //Given
      var sourcePower = 3.6;
      var electrodeCrossSectionalArea = 12.56;
      var argument = new EquationParameters(sourcePower, electrodeCrossSectionalArea);
      var alloyingElementId = 1;

      _transformationEquationMock.SetupGet(p => p.Entities)
        .Returns(DbSetMockFactory.SetupDbSetMoq(new AlloyingElementPartialTransformationEquation[0].AsQueryable()
        ).Object);

      var sut = GetSut();
      //When
      Func<Task<AlloyingElementPartialTransformationCoefficients>> func = async () => await sut.GetAlloyingElementPartialTransformationCoefficients(alloyingElementId, argument);
      //Then
      func.Should().Throw<ArgumentException>().WithMessage("For alloying element 1 not exists transformation equation");
    }

    [Test]
    public void ShouldFailGetAlloyingElementPartialTransformationCoefficientsWhenEquationIsEmptyTest()
    {
      //Given
      var sourcePower = 3.6;
      var electrodeCrossSectionalArea = 12.56;
      var argument = new EquationParameters(sourcePower, electrodeCrossSectionalArea);
      var alloyingElementId = 10;

      _transformationEquationMock.SetupGet(p => p.Entities)
        .Returns(DbSetMockFactory.SetupDbSetMoq(new AlloyingElementPartialTransformationEquation[] {
        new AlloyingElementPartialTransformationEquation
          {Id = 1, AlloyingElementId = 10, ToFerroalloyEquation = "0", ToKernelEquation = "0", ToGasEquation = "0", ToSlagEquation = ""},
        }.AsQueryable()
        ).Object);

      var sut = GetSut();
      //When
      Func<Task<AlloyingElementPartialTransformationCoefficients>> func = async () => await sut.GetAlloyingElementPartialTransformationCoefficients(alloyingElementId, argument);
      //Then
      func.Should().Throw<ArgumentException>().WithMessage("Equation cant be empty. If for alloying element partial transformation coefficient not exists, equation should be \"0\"");
    }

    [Test]
    public void ShouldFailGetAlloyingElementPartialTransformationCoefficientsWhenEquationIsInvalidTest()
    {
      //Given
      var sourcePower = 3.6;
      var electrodeCrossSectionalArea = 12.56;
      var argument = new EquationParameters(sourcePower, electrodeCrossSectionalArea);
      var alloyingElementId = 10;

      _transformationEquationMock.SetupGet(p => p.Entities)
        .Returns(DbSetMockFactory.SetupDbSetMoq(new AlloyingElementPartialTransformationEquation[] {
        new AlloyingElementPartialTransformationEquation
          {Id = 1, AlloyingElementId = 10, ToFerroalloyEquation = "(A-B)*2", ToKernelEquation = "0", ToGasEquation = "0", ToSlagEquation = "0"},
        }.AsQueryable()
        ).Object);

      var sut = GetSut();
      //When
      Func<Task<AlloyingElementPartialTransformationCoefficients>> func = async () => await sut.GetAlloyingElementPartialTransformationCoefficients(alloyingElementId, argument);
      //Then
      func.Should().Throw<Exception>().WithMessage("Equation [(A-B)*2] is incorrect");
    }

    #endregion

    public IComputationService GetSut() => _fixture.Create<ComputationService>();
  }
}
