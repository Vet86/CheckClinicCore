﻿using CheckClinic.Interfaces;
using CheckClinic.Model;
using CsQuery;
using System.Collections.Generic;

namespace CheckClinic.DataParser
{
    public class DistrictCollectionParser : IDistrictCollectionParser
    {
        public IList<IDistrict> ParseDistricts(string html)
        {
            CQ cq = CQ.CreateDocument(html);
            var districtListElement = cq["#district_list"];
            var x = districtListElement.Children();
            var districts = new List<IDistrict>();
            foreach (var elem in districtListElement.Children())
            {
                string dataId;
                if (elem.TryGetAttribute("data-id", out dataId) && !string.IsNullOrEmpty(dataId))
                {
                    districts.Add(new District(dataId, elem.FirstChild.ToString()));
                }
            }
            return districts;
        }
    }
}
