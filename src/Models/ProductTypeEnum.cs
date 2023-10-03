using System;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace YourMobileGuide.Models
{
    /// <summary>
    /// Defines an enum for the different types of celestial bodies
    /// </summary>
    public enum ProductTypeEnum
    {
        // Adding categories for ProductType list displayed on update page for admin to choose from

        /// <summary>
        /// Represents the year 2020
        /// </summary>
        [Display(Name = "2020")]
        Year2020 = 0,

        /// <summary>
        /// Represents the year 2021
        /// </summary>
        [Display(Name = "2021")]
        Year2021 = 1,

        /// <summary>
        /// Represents the year 2022
        /// </summary>
        [Display(Name = "2022")]
        Year2022 = 2,

        /// <summary>
        /// Represents the year 2023
        /// </summary>
        [Display(Name = "2023")]
        Year2023 = 3

    }
}

public static class Extensions
{
    /// <summary>
    /// An extension to ProductTypeEnum to get the display name for razor pages
    /// </summary>
    public static string GetName(this Enum productEnum)
    {
        return productEnum.GetType().GetMember(productEnum.ToString()).First()
                        .GetCustomAttribute<DisplayAttribute>().Name;
    }
}
