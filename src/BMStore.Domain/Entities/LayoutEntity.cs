using BMStore.Domain.Entities.Enums;

namespace BMStore.Domain.Entities;

public record LayoutEntity
{
    public int Id { get; init; }
    public Color MainColor { get; init; }
    public Color BackgroundColor { get; init; }
    public string BackgroundImage { get; init; } = string.Empty;
    public string SliderImage { get; init; } = string.Empty;
    public string LogoImage { get; init; } = string.Empty;
    public string HeaderStyle { get; init; } = string.Empty;
    public string FooterStyle { get; init; } = string.Empty;
    public string OtherStyling { get; init; } = string.Empty;
    public Font Font { get; init; }
}
       // სხვა სტილები(
       //კომპილერის ფუნქცია - ჰედერების შეცვლა.
       //ღილაკების/ ფონტის/კონკრეტულ ტაბებზე წარწერის შეცვლა/
       //განყოფილებების გარეთ გამოტანა ან დამალვა/
       //რა ინფო უნდა გამოჩნდეს გარეთ/ 
       //სლაიდერების ფოტოების შეცვლა ასევე დიზაინის ამოსარჩევი უნდა იყოს, 
       //სხვადასხვა გვერდზე მთავარი ფოტოების შეცვლა.)