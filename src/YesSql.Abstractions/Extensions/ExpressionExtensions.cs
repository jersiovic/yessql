// Microsoft Developer & Platform Evangelism
//=================================================================================== 
// THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
// EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED WARRANTIES 
// OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
//===================================================================================
// Copyright (c) Microsoft Corporation.  All Rights Reserved.
// This code is released under the terms of the MS-LPL license, 
// http://microsoftnlayerapp.codeplex.com/license
//===================================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace YesSql.Extensions
{
    /// <summary>
    /// Expression extensions methods for helping coding
    /// </summary>
    public static class ExpressionExtensions
    {
        public static string GetPath(this Expression expression)
        {
            string path;
            if (TryParsePath(expression, out path))
                return path;
            else
                return null;
        }

        static bool TryParsePath(Expression expression, out string path)
        {
            path = null;
            Expression expression2 = expression.RemoveConvert();
            MemberExpression expression3 = expression2 as MemberExpression;
            MethodCallExpression expression4 = expression2 as MethodCallExpression;
            if (expression3 != null)
            {
                string str2;
                string name = expression3.Member.Name;
                if (!TryParsePath(expression3.Expression, out str2))
                {
                    return false;
                }
                path = (str2 == null) ? name : (str2 + "." + name);
            }
            else if (expression4 != null)
            {
                if ((expression4.Method.Name == "Select") && (expression4.Arguments.Count == 2))
                {
                    string str3;
                    if (!TryParsePath(expression4.Arguments[0], out str3))
                    {
                        return false;
                    }
                    if (str3 != null)
                    {
                        LambdaExpression expression5 = expression4.Arguments[1] as LambdaExpression;
                        if (expression5 != null)
                        {
                            string str4;
                            if (!TryParsePath(expression5.Body, out str4))
                            {
                                return false;
                            }
                            if (str4 != null)
                            {
                                path = str3 + "." + str4;
                                return true;
                            }
                        }
                    }
                }
                return false;
            }
            return true;
        }


        /// <summary>
        /// RemoveConvert extension method. This method remove all "Convert" elements in
        /// a expression
        /// </summary>
        /// <param name="expression">The expression to remove convert</param>
        /// <returns>Expression with removed "Convert"</returns>
        public static Expression RemoveConvert(this Expression expression)
        {
            while ((expression != null) && ((expression.NodeType == ExpressionType.Convert) || (expression.NodeType == ExpressionType.ConvertChecked)))
            {
                expression = ((UnaryExpression)expression).Operand.RemoveConvert();
            }
            return expression;
        }

    }
}
