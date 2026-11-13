using Catalog.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.Domain.Entities
{
    public class Room
    {
        public int Id { get; set; }
        public string Number { get; set; } = string.Empty;
        public int RoomTypeId { get; set; }
        public RoomType? RoomType { get; set; }
        public decimal BasePricePerNight { get; set; }
        public string Description { get; set; } = string.Empty;
        public RoomStatus Status { get; set; } = RoomStatus.Active;

        public List<Amenity> Amenities { get; set; } = new();
    }
}
