﻿using System.ComponentModel;
using System.Reflection;

namespace BurgerRoyale.Domain.Enumerators
{
	public enum OrderStatus
	{
		[Description("Recebido")]
		Recebido,

		[Description("Em preparação")]
		EmPreparacao,

		[Description("Pronto")]
		Pronto,

		[Description("Finalizado")]
		Finalizado
	}
    public static class EnumExtensions
    {
        public static string GetDescription(this Enum value)
        {
            FieldInfo fieldInfo = value.GetType().GetField(value.ToString());
            DescriptionAttribute[] attributes = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];

            if (attributes != null && attributes.Length > 0)
            {
                return attributes[0].Description;
            }
            else
            {
                return value.ToString();
            }
        }
    }

}
