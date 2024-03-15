using FivePMShared.Models;

namespace FivePMSomewhereEngine;

public interface IFivePmSomewhereService
{
    FivePmModel GetApplicableTimeZones(DateTime? searchDate);
}
