using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaControllers.Helpers
{
    public static class MathHelper
    {
        /// <summary>
        /// Convierte un valor numérico entero entero con rango en un nuevo rango.
        /// </summary>
        /// <example> Ejemplo convirtiendo 50 del rango 0 - 50 al nuevo rango 0-10
        /// <code>
        ///     int nuevoValor = ConvertTo(0, 100, 0, 10, 50);
        ///     Console.WriteLine(nuevoValor); // Resultado: 0.5
        /// </code>
        /// </example>
        /// <param name="minValue">El valor mínimo del rango actual.</param>
        /// <param name="maxValue">El valor máximo del rango actual.</param>
        /// <param name="newMinValue">El valor mínimo del nuevo rango.</param>
        /// <param name="newMaxValue">El valor máximo del nuevo rango.</param>
        /// <param name="value">El valor a convertir en el nuevo rango.</param>
        /// <returns>El valor convertido en el nuevo rango.</returns>
        public static int Map(int minValue,int maxValue,int newMinValue,int newMaxValue,int value)
        {
            if(maxValue - minValue == 0)
            {
                return newMaxValue;
            }


            return ( (value-minValue) * (newMaxValue - newMinValue) / (maxValue - minValue) ) + newMinValue;
        }
        /// <summary>
        /// Convierte un valor numérico flotante entero con rango en un nuevo rango.
        /// </summary>
        /// <example> Ejemplo convirtiendo 50 del rango 0 - 50 al nuevo rango 0-10
        /// <code>
        ///     int nuevoValor = ConvertTo(0, 100, 0, 10, 50);
        ///     Console.WriteLine(nuevoValor); // Resultado: 0.5
        /// </code>
        /// </example>
        /// <param name="minValue">El valor mínimo del rango actual.</param>
        /// <param name="maxValue">El valor máximo del rango actual.</param>
        /// <param name="newMinValue">El valor mínimo del nuevo rango.</param>
        /// <param name="newMaxValue">El valor máximo del nuevo rango.</param>
        /// <param name="value">El valor a convertir en el nuevo rango.</param>
        /// <returns>El valor convertido en el nuevo rango.</returns>
        public static float Map(float minValue, float maxValue, float newMinValue, float newMaxValue, float value)
        {
            return ((value - minValue) * (newMaxValue - newMinValue) / (maxValue - minValue)) + newMinValue;
        }

    }
}
