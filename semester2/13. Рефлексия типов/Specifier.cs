using System;
using System.Linq;
using System.Reflection;

namespace Documentation
{
    public class Specifier<T> : ISpecifier
    {
        private Type type;
        private ILookup<string, MethodInfo> methods;
        public Specifier()
        {
            type = typeof(T);
            methods = type.GetMethods().ToLookup(x => x.Name);
        }
        
        public string GetApiDescription() => type.GetCustomAttributes<ApiDescriptionAttribute>()
            .FirstOrDefault()?.Description;

        public string[] GetApiMethodNames() => methods.SelectMany(x => x)
            .Where(x => x.GetCustomAttributes<ApiMethodAttribute>().Any())
            .Select(x => x.Name)
            .ToArray();

        public string GetApiMethodDescription(string methodName) => methods[methodName].FirstOrDefault()
            ?.GetCustomAttributes<ApiDescriptionAttribute>()
            .FirstOrDefault()?.Description;

        public string[] GetApiMethodParamNames(string methodName) => methods[methodName].FirstOrDefault()
            ?.GetParameters()
            .Select(x => x.Name)
            .ToArray();

        public string GetApiMethodParamDescription(string methodName, string paramName)
            => methods[methodName].FirstOrDefault()
                ?.GetParameters().FirstOrDefault(x => x.Name == paramName)
                ?.GetCustomAttributes<ApiDescriptionAttribute>()
                .FirstOrDefault()?.Description;

        public ApiParamDescription GetApiMethodParamFullDescription(string methodName, string paramName)
        {
            var attributes = methods[methodName].FirstOrDefault()
                ?.GetParameters().FirstOrDefault(x => x.Name == paramName)
                ?.GetCustomAttributes();
            var validationAttributes = attributes?.OfType<ApiIntValidationAttribute>().FirstOrDefault();

            return new ApiParamDescription
            { 
                ParamDescription = new CommonDescription
                {
                    Name = paramName,
                    Description = GetApiMethodParamDescription(methodName, paramName)
                },

                Required = attributes?.OfType<ApiRequiredAttribute>().FirstOrDefault()?.Required ?? false,
                MinValue = validationAttributes?.MinValue,
                MaxValue = validationAttributes?.MaxValue
            };
        }

        public ApiMethodDescription GetApiMethodFullDescription(string methodName)
            => (methods[methodName].FirstOrDefault()?.GetCustomAttributes<ApiMethodAttribute>().Any() ?? false)
                ? new ApiMethodDescription
                {
                    MethodDescription = new CommonDescription
                    {
                        Name = methodName,
                        Description = GetApiMethodDescription(methodName)
                    },
                    ParamDescriptions = methods[methodName].FirstOrDefault()
                        ?.GetParameters()
                        .Select(p => GetApiMethodParamFullDescription(methodName, p.Name))
                        .ToArray(),
                    ReturnDescription = ReturnParamFullDescription(methodName)
                }
                : default;

        private ApiParamDescription ReturnParamFullDescription(string methodName)
        {
            var attributes = methods[methodName].FirstOrDefault()
                ?.ReturnParameter?.GetCustomAttributes();
            if (!attributes.Any())
                return null;
            var validationAttributes = attributes?.OfType<ApiIntValidationAttribute>().FirstOrDefault();

            return new ApiParamDescription
            { 
                ParamDescription = new CommonDescription
                {
                    Description = attributes?.OfType<ApiDescriptionAttribute>().FirstOrDefault()?.Description
                },

                Required = attributes?.OfType<ApiRequiredAttribute>().FirstOrDefault().Required ?? false,
                MinValue = validationAttributes?.MinValue,
                MaxValue = validationAttributes?.MaxValue
            };
        }
    }
}