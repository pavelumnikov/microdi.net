﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using JetBrains.Annotations;

namespace microDI.Internal.Assert
{
    [DebuggerStepThrough]
    public static class Assertions
    {
        /// <summary>
        /// Checks that <c>value</c> is not null.
        /// </summary>
        /// <typeparam name="T">Type of input <c>value</c> parameter</typeparam>
        /// <param name="value">Parameter to be inspected</param>
        /// <param name="paramName">Name of parameter</param>
        /// <returns><c>value</c> from first parameter</returns>
        [AssertionMethod]
        [ContractAnnotation("value: null => halt")]
        public static T NotNull<T>( T value,
            [AssertionCondition(AssertionConditionType.IsNull), InvokerParameterName, NotNull] string paramName)
        {
            if (!ReferenceEquals(value, null))
                return value;

            NotNullOrEmpty(paramName, paramName);
            throw new ArgumentNullException(paramName);
        }

        /// <summary>
        /// Checks if collection reference is null, or collection is empty itself.
        /// </summary>
        /// <typeparam name="T">Containing type of collection.</typeparam>
        /// <param name="value">Reference to read-only collection.</param>
        /// <param name="paramName">Name of parameter to be checked against.</param>
        /// <returns><c>value</c> from first parameter</returns>
        [AssertionMethod]
        [ContractAnnotation("value: null => halt")]
        public static IReadOnlyCollection<T> NotNullOrEmpty<T>(
            [AssertionCondition(AssertionConditionType.IsNull)] IReadOnlyCollection<T> value,
            [InvokerParameterName, NotNull] string paramName)
        {
            NotNull(value, paramName);

            if (value.Any())
                return value;

            throw new ArgumentException("Collection is empty!", paramName);
        }

        /// <summary>
        /// Checks if string reference is null, or string is empty.
        /// </summary>
        /// <param name="value">Reference to string.</param>
        /// <param name="paramName">Name of parameter to be checked against.</param>
        /// <returns><c>value</c> from first parameter</returns>
        [AssertionMethod]
        [ContractAnnotation("value: null => halt")]
        public static string NotNullOrEmpty( 
            string value, 
            [InvokerParameterName, NotNull] string paramName)
        {
            Exception e = null;

            if (ReferenceEquals(value, null))
                e = new ArgumentNullException(paramName);
            else if (value.Trim().Length == 0)
                e = new ArgumentException("String is empty!", paramName);

            if (e != null)
                throw e;

            return value;
        }

        /// <summary>
        /// Checks 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="prediction"></param>
        /// <param name="paramName"></param>
        /// <returns>Result of expression execution.</returns>
        [AssertionMethod]
        [ContractAnnotation("prediction: false => halt")]
        public static T Expression<T>( 
            T value,
            [AssertionCondition(AssertionConditionType.IsNull), NotNull] Func<T, bool> prediction,
            [InvokerParameterName, NotNull] string paramName)
        {
            NotNull(prediction, nameof(prediction));

            if (prediction.Invoke(value))
                return value;

            throw new Exception("prediction is not true!");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prediction"></param>
        [AssertionMethod]
        [ContractAnnotation("prediction: false => halt")]
        public static void Expression(
            [AssertionCondition(AssertionConditionType.IsNull), NotNull] Func<bool> prediction)
        {
            NotNull(prediction, nameof(prediction));

            if (!prediction.Invoke())
                throw new Exception("prediction is not true!");
        }
    }
}
