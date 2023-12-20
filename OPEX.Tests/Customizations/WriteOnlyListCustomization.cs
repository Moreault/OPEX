using System.Collections;
using AutoFixture.Kernel;
using ToolBX.Eloquentest.Customizations;

namespace OPEX.Tests.Customizations;

[AutoCustomization]
public sealed class WriteOnlyListCustomization : ICustomization
{
    public void Customize(IFixture fixture)
    {
        fixture.Customizations.Add(new WriteOnlyListSpecimenBuilder());
    }
}

public class WriteOnlyListSpecimenBuilder : ISpecimenBuilder
{
    public object Create(object request, ISpecimenContext context)
    {
        var type = request as Type;
        if (type != null && type.IsGenericType && type.GetGenericTypeDefinition() == typeof(WriteOnlyList<>))
        {
            var elementType = type.GetGenericArguments()[0];
            var listType = typeof(WriteOnlyList<>).MakeGenericType(elementType);
            var list = Activator.CreateInstance(listType);

            // You can customize the population logic based on your requirements.
            // Here, we assume that the WriteOnlyList<T> has an "Add" method.
            var addMethod = type.GetMethod("Add");
            if (addMethod != null)
            {
                var elements = new List<object> { context.Resolve(elementType), context.Resolve(elementType), context.Resolve(elementType) };
                foreach (var element in elements)
                {
                    addMethod.Invoke(list, new[] { element });
                }
            }

            return list;
        }

        return new NoSpecimen();
    }
}