#if !WindowsCE
using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Unme.Common
{
	/// <summary>
	/// Provides convenience methods for working with Reflection.
	/// </summary>
	/// <typeparam name="T">The type to reflect on.</typeparam>
	public static class Reflect<T>
	{
		/// <summary>
		/// Gets the name of the property.
		/// </summary>
		/// <typeparam name="TProperty">The type of the property.</typeparam>
		/// <param name="propertyAccessExpression">The property access expression.</param>
		/// <returns></returns>
		/// <remarks>
		/// This method is significantly slower than simply using property name strings. But the
		/// benefit of refactoring support, outweighs the cost of any single call. DO cache the result
		/// of this method for performance.
		/// </remarks>
		public static string GetPropertyName<TProperty>(Expression<Func<T, TProperty>> propertyAccessExpression)
		{
			// get MemberExpression from the lambda expression
			MemberExpression expr = propertyAccessExpression.Body as MemberExpression;
			if (expr == null)
				throw new ArgumentException("Expression must be a property access.", "propertyAccessExpression");

			// verify that the member is a property
			if ((expr.Member.MemberType & MemberTypes.Property) != MemberTypes.Property)
				throw new ArgumentException("Accessed member must be a property.", "propertyAccessExpression");

			return expr.Member.Name;
		}
	}
}
#endif