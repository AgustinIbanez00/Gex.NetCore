using Humanizer;

namespace Gex.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class GexDescriptionAttribute : Attribute
    {
        public GexDescriptionAttribute(string name, GrammaticalGender gender)
        {
            Name = name;
            Gender = gender;
        }

        public string Name { get; set; }
        public GrammaticalGender Gender { get; set; }
    }
}
