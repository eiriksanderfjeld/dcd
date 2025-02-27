using api.Models;

namespace api.Dtos;

public class CaseDto
{
    public Guid Id { get; set; }
    public Guid ProjectId { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public bool ReferenceCase { get; set; }
    public ArtificialLift ArtificialLift { get; set; }
    public ProductionStrategyOverview ProductionStrategyOverview { get; set; }
    public int ProducerCount { get; set; }
    public int GasInjectorCount { get; set; }
    public int WaterInjectorCount { get; set; }
    public double FacilitiesAvailability { get; set; }
    public double CapexFactorFeasibilityStudies { get; set; }
    public double CapexFactorFEEDStudies { get; set; }
    public double NPV { get; set; }
    public double BreakEven { get; set; }
    public string? Host { get; set; }

    public DateTimeOffset DGADate { get; set; }
    public DateTimeOffset DGBDate { get; set; }
    public DateTimeOffset DGCDate { get; set; }
    public DateTimeOffset APXDate { get; set; }
    public DateTimeOffset APZDate { get; set; }
    public DateTimeOffset DG0Date { get; set; }
    public DateTimeOffset DG1Date { get; set; }
    public DateTimeOffset DG2Date { get; set; }
    public DateTimeOffset DG3Date { get; set; }
    public DateTimeOffset DG4Date { get; set; }
    public DateTimeOffset CreateTime { get; set; }
    public DateTimeOffset ModifyTime { get; set; }

    public CessationWellsCostDto? CessationWellsCost { get; set; }
    public CessationWellsCostOverrideDto? CessationWellsCostOverride { get; set; }
    public CessationOffshoreFacilitiesCostDto? CessationOffshoreFacilitiesCost { get; set; }
    public CessationOffshoreFacilitiesCostOverrideDto? CessationOffshoreFacilitiesCostOverride { get; set; }

    public TotalFeasibilityAndConceptStudiesDto? TotalFeasibilityAndConceptStudies { get; set; }
    public TotalFeasibilityAndConceptStudiesOverrideDto? TotalFeasibilityAndConceptStudiesOverride { get; set; }
    public TotalFEEDStudiesDto? TotalFEEDStudies { get; set; }
    public TotalFEEDStudiesOverrideDto? TotalFEEDStudiesOverride { get; set; }

    public WellInterventionCostProfileDto? WellInterventionCostProfile { get; set; }
    public WellInterventionCostProfileOverrideDto? WellInterventionCostProfileOverride { get; set; }
    public OffshoreFacilitiesOperationsCostProfileDto? OffshoreFacilitiesOperationsCostProfile { get; set; }
    public OffshoreFacilitiesOperationsCostProfileOverrideDto? OffshoreFacilitiesOperationsCostProfileOverride { get; set; }

    public Guid DrainageStrategyLink { get; set; }
    public Guid WellProjectLink { get; set; }
    public Guid SurfLink { get; set; }
    public Guid SubstructureLink { get; set; }
    public Guid TopsideLink { get; set; }
    public Guid TransportLink { get; set; }
    public Guid ExplorationLink { get; set; }

    public double Capex { get; set; }
    public CapexYear? CapexYear { get; set; }
    public CessationCostDto? CessationCost { get; set; }
    public string? SharepointFileId { get; set; }
    public string? SharepointFileName { get; set; }
    public string? SharepointFileUrl { get; set; }
    public bool HasChanges { get; set; }
}

public class CessationCostDto : TimeSeriesCostDto
{
}
public class CessationWellsCostDto : TimeSeriesCostDto
{
}
public class CessationWellsCostOverrideDto : TimeSeriesCostDto, ITimeSeriesOverrideDto
{
    public bool Override { get; set; }
}
public class CessationOffshoreFacilitiesCostDto : TimeSeriesCostDto
{
}
public class CessationOffshoreFacilitiesCostOverrideDto : TimeSeriesCostDto, ITimeSeriesOverrideDto
{
    public bool Override { get; set; }
}

public class OpexCostProfileDto : TimeSeriesCostDto
{
}

public class WellInterventionCostProfileDto : TimeSeriesCostDto
{
}

public class WellInterventionCostProfileOverrideDto : TimeSeriesCostDto, ITimeSeriesOverrideDto
{
    public bool Override { get; set; }
}

public class OffshoreFacilitiesOperationsCostProfileDto : TimeSeriesCostDto
{
}

public class OffshoreFacilitiesOperationsCostProfileOverrideDto : TimeSeriesCostDto, ITimeSeriesOverrideDto
{
    public bool Override { get; set; }
}

public class StudyCostProfileDto : TimeSeriesCostDto
{
}
public class TotalFeasibilityAndConceptStudiesDto : TimeSeriesCostDto
{
}
public class TotalFeasibilityAndConceptStudiesOverrideDto : TimeSeriesCostDto, ITimeSeriesOverrideDto
{
    public bool Override { get; set; }
}
public class TotalFEEDStudiesDto : TimeSeriesCostDto
{
}
public class TotalFEEDStudiesOverrideDto : TimeSeriesCostDto, ITimeSeriesOverrideDto
{
    public bool Override { get; set; }
}

public class CessationCostWrapperDto
{
    public CessationCostDto? CessationCostDto { get; set; }
    public CessationWellsCostDto? CessationWellsCostDto { get; set; }
    public CessationOffshoreFacilitiesCostDto? CessationOffshoreFacilitiesCostDto { get; set; }
}

public class OpexCostProfileWrapperDto
{
    public OpexCostProfileDto? OpexCostProfileDto { get; set; }
    public WellInterventionCostProfileDto? WellInterventionCostProfileDto { get; set; }
    public OffshoreFacilitiesOperationsCostProfileDto? OffshoreFacilitiesOperationsCostProfileDto { get; set; }
}

public class StudyCostProfileWrapperDto
{
    public StudyCostProfileDto? StudyCostProfileDto { get; set; }
    public TotalFeasibilityAndConceptStudiesDto? TotalFeasibilityAndConceptStudiesDto { get; set; }
    public TotalFEEDStudiesDto? TotalFEEDStudiesDto { get; set; }
}

public class CapexYear
{
    public double[]? Values { get; set; }
    public int? StartYear { get; set; }
}
