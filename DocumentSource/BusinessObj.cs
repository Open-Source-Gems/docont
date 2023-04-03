using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace DocumentSource
{
 // using System.Xml.Serialization;
 // XmlSerializer serializer = new XmlSerializer(typeof(Счет));
 // using (StringReader reader = new StringReader(xml))
 // {
 //    var test = (Счет)serializer.Deserialize(reader);
 // }

    [XmlRoot(ElementName = "Товар")]
    public class Товар
    {

        [XmlElement(ElementName = "Наименование")]
        public string Наименование { get; set; }

        [XmlElement(ElementName = "Количество")]
        public int Количество { get; set; }
    }

    [XmlRoot(ElementName = "Товары")]
    public class Товары
    {

        [XmlElement(ElementName = "Товар")]
        public List<Товар> Товар { get; set; }
    }

    [XmlRoot(ElementName = "Счет")]
    public class Счет
    {
        [XmlElement(ElementName = "Номер")]
        public int Номер { get; set; }

        [XmlElement(ElementName = "Дата")]
        public DateTime Дата { get; set; }

        [XmlElement(ElementName = "Поставщик")]
        public string Поставщик { get; set; }

        [XmlElement(ElementName = "Сумма")]
        public double Сумма { get; set; }

        [XmlElement(ElementName = "Отмена")]
        public bool Отмена { get; set; }

        [XmlElement(ElementName = "Товары")]
        public Товары Товары { get; set; }
    }

    public static class Пример
    {
        public static string XML => @"<Счет>
<Номер>1</Номер>
<Дата>2023-01-01</Дата>
<Поставщик>ООО Рога и Копыта</Поставщик>
<Сумма>100.78</Сумма>
<Отмена>false</Отмена>
<Товары>
<Товар>
<Наименование>Резистор</Наименование>
<Количество>10</Количество>
</Товар>
<Товар>
<Наименование>Конденсатор</Наименование>
<Количество>50</Количество>
</Товар>
<Товар>
<Наименование>Радиатор</Наименование>
<Количество>100</Количество>
</Товар>
</Товары>
</Счет>";

 }
}
