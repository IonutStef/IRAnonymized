﻿using Newtonsoft.Json;

namespace IRAnonymized.Assignment.WebApi.Models
{
    /// <summary>
    /// Store item model to be displayed to the end user.
    /// </summary>
    public class StoreItemModel
    {
        public string Key { get; set; }

        [JsonProperty(PropertyName = "ArtikelCode")]
        public string ArticleCode { get; set; }

        public string ColorCode { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }

        public int DiscountPrice { get; set; }

        [JsonProperty(PropertyName = "DeliveredIn")]
        public string DeliveredInAddress { get; set; }

        [JsonProperty(PropertyName = "Q1")]
        public string SizeGroup { get; set; }

        public int Size { get; set; }

        public string Color { get; set; }
    }
}