using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Components.Model
{
    public class Vector
    {
        public int XCoordinate { get; set; }
        public int YCoordinate { get; set; }

        /// <summary>
        /// Returns whether both vectors are the same
        /// </summary>
        /// <param name="lhs">Left hand side of the operator</param>
        /// <param name="rhs">Right hand side of the operator</param>
        /// <returns>Boolean value describing whether they are the same</returns>
        public static bool operator ==(Vector lhs, Vector rhs)
        {
            if (lhs.YCoordinate == rhs.YCoordinate && lhs.XCoordinate == rhs.XCoordinate)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Returns whether the vectors aren't the same
        /// </summary>
        /// <param name="lhs">Left hand side of the operator</param>
        /// <param name="rhs">Right hand side of the operator</param>
        /// <returns>Boolean value describing whether they aren't the same</returns>
        public static bool operator !=(Vector lhs, Vector rhs)
        {
            if (lhs.YCoordinate == rhs.YCoordinate && lhs.XCoordinate == rhs.XCoordinate)
            {
                return false;
            }
            return true;
        }

        public override bool Equals(object obj)
        {
            return obj is Vector vector &&
                   YCoordinate == vector.YCoordinate &&
                   XCoordinate == vector.XCoordinate;
        }

        public override int GetHashCode()
        {
            var hashCode = -1219734581;
            hashCode = hashCode * -1521134295 + XCoordinate.GetHashCode();
            hashCode = hashCode * -1521134295 + YCoordinate.GetHashCode();
            return hashCode;
        }
    }
}
