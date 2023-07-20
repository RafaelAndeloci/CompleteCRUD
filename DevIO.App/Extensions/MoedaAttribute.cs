using System.ComponentModel.DataAnnotations;
using System.Globalization;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.Extensions.Localization;

namespace DevIO.App.Extensions;

public class MoedaAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        try
        {
            var unused = Convert.ToDecimal(value, new CultureInfo("pt-BR"));
        }
        catch (Exception)
        {
            return new ValidationResult("Moeda em formato inválido");
        }

        return ValidationResult.Success;
    }
}

public class MoedaAttributeAdapter : AttributeAdapterBase<MoedaAttribute>
{
    public MoedaAttributeAdapter(MoedaAttribute attribute, IStringLocalizer stringLocalizer) : base(attribute, stringLocalizer) { }

    public override void AddValidation(ClientModelValidationContext context)
    {
        if (context == null) throw new ArgumentNullException(nameof(context));

        MergeAttribute(context.Attributes, "data-val", "true");
        MergeAttribute(context.Attributes, "data-val-moeda", GetErrorMessage(context));
        MergeAttribute(context.Attributes, "data-val-number", GetErrorMessage(context));
    }

    public override string GetErrorMessage(ModelValidationContextBase validationContext)
    {
        return "Moeda em formato inválido";
    }
}

public class MoedaValidationAttributeAdapterProvider : IValidationAttributeAdapterProvider
{
    private readonly IValidationAttributeAdapterProvider _baseProvider = new ValidationAttributeAdapterProvider();
    
    public IAttributeAdapter GetAttributeAdapter(ValidationAttribute attribute, IStringLocalizer stringLocalizer)
    {
        return attribute is MoedaAttribute moedaAttribute ? 
            new MoedaAttributeAdapter(moedaAttribute, stringLocalizer) : _baseProvider.GetAttributeAdapter(attribute, stringLocalizer);
    }
}