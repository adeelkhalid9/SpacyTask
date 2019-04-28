using System;

namespace InterfaceService.Models
{
    public class TextToken
    {
        public int Start { get; set; }
        public int End { get; set; }
        public string Label { get; set; }
    }
}