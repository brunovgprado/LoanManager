using LoanManager.Domain.Properties;
using System;

namespace LoanManager.Domain.ValueObjects.Game
{
    public struct Genre : IComparable<Genre>
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
                    throw new ArgumentOutOfRangeException(Resources.GenreAtLeastThreeLettersAndFirstLetterUppercaseIsMandatory);
                name = value;
            }
        }

        public Genre(string name)
        {
            if (name.Length < 3 || Char.IsLower(name[0]))
                throw new ArgumentOutOfRangeException(Resources.GenreAtLeastThreeLettersAndFirstLetterUppercaseIsMandatory);
            this.name = name;
        }

        public static Genre Parse(string genre)
        {
            return new Genre(genre);
        }

        public static implicit operator String(Genre genre)
        {
            return genre.Name;
        }

        public static implicit operator Genre(string genre)
        {
            return new Genre(genre);
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

        public int CompareTo(Genre other)
        {
            return this.Name.CompareTo(other.Name);
        }
    }
}
