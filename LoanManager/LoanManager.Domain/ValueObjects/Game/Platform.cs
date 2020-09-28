using LoanManager.Domain.Properties;
using System;

namespace LoanManager.Domain.ValueObjects.Game
{
    public struct Platform : IComparable<Platform>
    {
        private string name;
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                if (value.Length < 3 || Char.IsLower(value[0]))
                    throw new ArgumentOutOfRangeException(Resources.PlatformAtLeastTwoLettersAndFirstLetterUppercaseIsMandatory);
                name = value;
            }
        }

        public Platform(string name)
        {
            if (name.Length < 3 || Char.IsLower(name[0]))
                throw new ArgumentOutOfRangeException(Resources.PlatformAtLeastTwoLettersAndFirstLetterUppercaseIsMandatory);
            this.name = name;
        }

        public static Platform Parse(string platform)
        {
            return new Platform(platform);
        }

        public static implicit operator String(Platform platform)
        {
            return platform.Name;
        }

        public static implicit operator Platform(string platform)
        {
            return new Platform(platform);
        }

        public override bool Equals(object obj)
        {
            return this.ToString() == obj.ToString();
        }

        public override int GetHashCode()
        {
            return this.Name.GetHashCode();
        }

        public override string ToString()
        {
            return Name;
        }

        public int CompareTo(Platform other)
        {
            return this.Name.CompareTo(other.Name);
        }
    }
}
