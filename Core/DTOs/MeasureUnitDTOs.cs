using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DTOs
{
    public class MeasureUnitDTO
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string Abbreviation { get; set; } = null!;
    }
}
