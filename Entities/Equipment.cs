//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EquipmentRentAPP.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class Equipment
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int StatusID { get; set; }
        [Newtonsoft.Json.JsonIgnore]
        public virtual EquipmentStatus EquipmentStatus { get; set; }
    }
}
