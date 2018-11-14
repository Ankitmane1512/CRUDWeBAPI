using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Bannerflow.Models
{
    public class Banner
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Html { get; set; }
        public string BannerName { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }
    }
}
