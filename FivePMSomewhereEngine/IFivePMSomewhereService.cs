﻿using FivePMSomewhereShared.Models;

namespace FivePMSomewhereEngine;

public interface IFivePMSomewhereService
{
    FivePmModel GetApplicableTimeZones(DateTime? searchDate = null, string? currentCountry = null);
    string GetCountries(IEnumerable<string> countries);
}
