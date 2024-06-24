using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using TimeZone_Converter_using_ASP.NETCore.Models;
using TimeZone_Converter_using_ASP.NETCore.Services;
using TimeZoneConverter;

namespace TimeZone_Converter_using_ASP.NETCore.Pages
{
    public class IndexModel : PageModel
    {
        private readonly TimeConversionService _timeConversionService;

        public IndexModel(TimeConversionService timeConversionService)
        {
            _timeConversionService = timeConversionService;
            TimeConverterModel = new TimeConverterModel();

            // Fetch and sort the time zones with country names
            TimeZones = TZConvert.KnownIanaTimeZoneNames
                        .Select(tz => new
                        {
                            Id = tz,
                            DisplayName = $"{tz} ({TZConvert.GetTimeZoneInfo(tz).DisplayName})"
                        })
                        .OrderBy(tz => tz.DisplayName)
                        .ToList()
                        .Select(tz => tz.DisplayName);
        }

        [BindProperty]
        public TimeConverterModel TimeConverterModel { get; set; }
        public IEnumerable<string> TimeZones { get; set; }

        public void OnGet()
        {
            TimeConverterModel.TargetDateTime = DateTime.MinValue;
        }

        public void OnPost()
        {
            if (ModelState.IsValid)
            {
                // Only take the time part from the SourceDateTime
                var sourceDateTime = new DateTime(1, 1, 1,
                    TimeConverterModel.SourceDateTime.Hour,
                    TimeConverterModel.SourceDateTime.Minute,
                    TimeConverterModel.SourceDateTime.Second);

                TimeConverterModel.TargetDateTime = _timeConversionService.ConvertTime(
                    sourceDateTime,
                    TimeConverterModel.SourceTimeZone.Split(" (")[0],  // Extracting the original time zone ID
                    TimeConverterModel.TargetTimeZone.Split(" (")[0]   // Extracting the original time zone ID
                );
            }
        }
    }
}