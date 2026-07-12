using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs
{
    public class MeasureUnitDTO
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string Abbreviation { get; set; } = null!;
    }

    public class CreateMeasureUnitDTO
    {
        public required string Name { get; set; }
        public string Abbreviation { get; set; } = null!;
    }

    public class UpdateMeasureUnitDTO
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string Abbreviation { get; set; } = null!;
    }
}
