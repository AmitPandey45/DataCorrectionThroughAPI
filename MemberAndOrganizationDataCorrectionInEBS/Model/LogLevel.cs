using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemberAndOrganizationDataCorrectionInEBS.Model
{
    /// <summary>
    /// Class _logLevel. This class cannot be inherited.
    /// </summary>
    public sealed class LogLevel : IComparable
    {
        /// <summary>
        /// Trace log level.
        /// </summary>
        public static readonly LogLevel TRACE = new LogLevel("TRACE", 0);

        /// <summary>
        /// Debug log level.
        /// </summary>
        public static readonly LogLevel DEBUG = new LogLevel("DEBUG", 1);

        /// <summary>
        /// Info log level.
        /// </summary>
        public static readonly LogLevel INFO = new LogLevel("INFO", 2);

        /// <summary>
        /// Warn log level.
        /// </summary>
        public static readonly LogLevel WARN = new LogLevel("WARN", 3);

        /// <summary>
        /// Error log level.
        /// </summary>
        public static readonly LogLevel ERROR = new LogLevel("ERROR", 4);

        /// <summary>
        /// Fatal log level.
        /// </summary>
        public static readonly LogLevel FATAL = new LogLevel("FATAL", 5);

        /// <summary>
        /// Off log level.
        /// </summary>
        public static readonly LogLevel Off = new LogLevel("Off", 6);

        /// <summary>
        /// The _ordinal
        /// </summary>
        private readonly int _ordinal;

        /// <summary>
        /// The _name
        /// </summary>
        private readonly string _name;

        /// <summary>
        /// Initializes a new instance of the <see cref="LogLevel"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="ordinal">The ordinal.</param>
        private LogLevel(string name, int ordinal)
        {
            this._name = name;
            this._ordinal = ordinal;
        }

        /// <summary>
        /// Gets the name of the log level.
        /// </summary>
        /// <value>The name.</value>
        public string Name
        {
            get { return this._name; }
        }

        /// <summary>
        /// Gets the maximum level.
        /// </summary>
        /// <value>The maximum level.</value>
        internal static LogLevel MaxLevel
        {
            get { return FATAL; }
        }

        /// <summary>
        /// Gets the minimum level.
        /// </summary>
        /// <value>The minimum level.</value>
        internal static LogLevel MinLevel
        {
            get { return TRACE; }
        }

        /// <summary>
        /// Gets the ordinal of the log level.
        /// </summary>
        /// <value>The ordinal.</value>
        internal int Ordinal
        {
            get { return this._ordinal; }
        }

        /// <summary>
        /// Compares two <see cref="LogLevel"/> objects 
        /// and returns a value indicating whether 
        /// the first one is equal to the second one.
        /// </summary>
        /// <param name="level1">The first level.</param>
        /// <param name="level2">The second level.</param>
        /// <returns>The value of <c>level1.Ordinal == level2.Ordinal</c>.</returns>
        public static bool operator ==(LogLevel level1, LogLevel level2)
        {
            if (ReferenceEquals(level1, null))
            {
                return ReferenceEquals(level2, null);
            }

            if (ReferenceEquals(level2, null))
            {
                return false;
            }

            return level1.Ordinal == level2.Ordinal;
        }

        /// <summary>
        /// Compares two <see cref="LogLevel"/> objects 
        /// and returns a value indicating whether 
        /// the first one is not equal to the second one.
        /// </summary>
        /// <param name="level1">The first level.</param>
        /// <param name="level2">The second level.</param>
        /// <returns>The value of <c>level1.Ordinal != level2.Ordinal</c>.</returns>
        public static bool operator !=(LogLevel level1, LogLevel level2)
        {
            if (ReferenceEquals(level1, null))
            {
                return !ReferenceEquals(level2, null);
            }

            if (ReferenceEquals(level2, null))
            {
                return true;
            }

            return level1.Ordinal != level2.Ordinal;
        }

        /// <summary>
        /// Compares two <see cref="LogLevel"/> objects 
        /// and returns a value indicating whether 
        /// the first one is greater than the second one.
        /// </summary>
        /// <param name="level1">The first level.</param>
        /// <param name="level2">The second level.</param>
        /// <returns>The value of <c>level1.Ordinal &gt; level2.Ordinal</c>.</returns>
        public static bool operator >(LogLevel level1, LogLevel level2)
        {
            if (level1 == null)
            {
                throw new ArgumentNullException(nameof(level1));
            }

            if (level2 == null)
            {
                throw new ArgumentNullException(nameof(level2));
            }

            return level1.Ordinal > level2.Ordinal;
        }

        /// <summary>
        /// Compares two <see cref="LogLevel"/> objects 
        /// and returns a value indicating whether 
        /// the first one is greater than or equal to the second one.
        /// </summary>
        /// <param name="level1">The first level.</param>
        /// <param name="level2">The second level.</param>
        /// <returns>The value of <c>level1.Ordinal &gt;= level2.Ordinal</c>.</returns>
        public static bool operator >=(LogLevel level1, LogLevel level2)
        {
            if (level1 == null)
            {
                throw new ArgumentNullException(nameof(level1));
            }

            if (level2 == null)
            {
                throw new ArgumentNullException(nameof(level2));
            }

            return level1.Ordinal >= level2.Ordinal;
        }

        /// <summary>
        /// Compares two <see cref="LogLevel"/> objects 
        /// and returns a value indicating whether 
        /// the first one is less than the second one.
        /// </summary>
        /// <param name="level1">The first level.</param>
        /// <param name="level2">The second level.</param>
        /// <returns>The value of <c>level1.Ordinal &lt; level2.Ordinal</c>.</returns>
        public static bool operator <(LogLevel level1, LogLevel level2)
        {
            if (level1 == null)
            {
                throw new ArgumentNullException(nameof(level1));
            }

            if (level2 == null)
            {
                throw new ArgumentNullException(nameof(level2));
            }

            return level1.Ordinal < level2.Ordinal;
        }

        /// <summary>
        /// Compares two <see cref="LogLevel"/> objects 
        /// and returns a value indicating whether 
        /// the first one is less than or equal to the second one.
        /// </summary>
        /// <param name="level1">The first level.</param>
        /// <param name="level2">The second level.</param>
        /// <returns>The value of <c>level1.Ordinal &lt;= level2.Ordinal</c>.</returns>
        public static bool operator <=(LogLevel level1, LogLevel level2)
        {
            if (level1 == null)
            {
                throw new ArgumentNullException(nameof(level1));
            }

            if (level2 == null)
            {
                throw new ArgumentNullException(nameof(level2));
            }

            return level1.Ordinal <= level2.Ordinal;
        }

        /// <summary>
        /// Gets the <see cref="LogLevel"/> that corresponds to the specified ordinal.
        /// </summary>
        /// <param name="ordinal">The ordinal.</param>
        /// <returns>The <see cref="LogLevel"/> instance. For 0 it returns <see cref="LogLevel.Trace"/>, 1 gives <see cref="LogLevel.Debug"/> and so on.</returns>
        public static LogLevel FromOrdinal(int ordinal)
        {
            switch (ordinal)
            {
                case 0:
                    return TRACE;
                case 1:
                    return DEBUG;
                case 2:
                    return INFO;
                case 3:
                    return WARN;
                case 4:
                    return ERROR;
                case 5:
                    return FATAL;
                case 6:
                    return Off;

                default:
                    throw new ArgumentException("Invalid ordinal.");
            }
        }

        /// <summary>
        /// Returns the <see cref="T:NLog._logLevel"/> that corresponds to the supplied <see langword="string" />.
        /// </summary>
        /// <param name="levelName">The textual representation of the log level.</param>
        /// <returns>The enumeration value.</returns>
        public static LogLevel FromString(string levelName)
        {
            if (levelName == null)
            {
                throw new ArgumentNullException(nameof(levelName));
            }

            if (levelName.Equals("TRACE", StringComparison.OrdinalIgnoreCase))
            {
                return TRACE;
            }

            if (levelName.Equals("DEBUG", StringComparison.OrdinalIgnoreCase))
            {
                return DEBUG;
            }

            if (levelName.Equals("INFO", StringComparison.OrdinalIgnoreCase))
            {
                return INFO;
            }

            if (levelName.Equals("WARN", StringComparison.OrdinalIgnoreCase))
            {
                return WARN;
            }

            if (levelName.Equals("ERROR", StringComparison.OrdinalIgnoreCase))
            {
                return ERROR;
            }

            if (levelName.Equals("FATAL", StringComparison.OrdinalIgnoreCase))
            {
                return FATAL;
            }

            if (levelName.Equals("Off", StringComparison.OrdinalIgnoreCase))
            {
                return Off;
            }

            throw new ArgumentException("Unknown log level: " + levelName);
        }

        /// <summary>
        /// Returns a string representation of the log level.
        /// </summary>
        /// <returns>Log level name.</returns>
        public override string ToString()
        {
            return this.Name;
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return this.Ordinal;
        }

        /// <summary>
        /// Determines whether the specified <see cref="object"/> is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to compare with this instance.</param>
        /// <returns>
        /// Value of <c>true</c> if the specified <see cref="object"/> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="T:System.NullReferenceException">
        /// The <paramref name="obj"/> parameter is null.
        /// </exception>
        public override bool Equals(object obj)
        {
            var logLevel = obj as LogLevel;

            if (logLevel == null)
            {
                return false;
            }

            return this.Ordinal == logLevel.Ordinal;
        }

        /// <summary>
        /// Compares the level to the other <see cref="LogLevel"/> object.
        /// </summary>
        /// <param name="obj">
        /// The object object.
        /// </param>
        /// <returns>
        /// A value less than zero when this logger's <see cref="Ordinal"/> is 
        /// less than the other logger's ordinal, 0 when they are equal and 
        /// greater than zero when this ordinal is greater than the
        /// other ordinal.
        /// </returns>
        public int CompareTo(object obj)
        {
            var level = (LogLevel)obj;
            return this.Ordinal - level.Ordinal;
        }
    }
}
