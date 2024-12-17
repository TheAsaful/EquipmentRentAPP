using System;
using EquipmentRentAPP.Entities;

namespace EquipmentRentAPP.Models
{
    public class EquipmentUpdater
    {
        public int EquipmentID { get; set; }
        public string FieldName { get; set; }
        public string NewValue { get; set; }

        private readonly RentEquipmentEntities _db;

        public EquipmentUpdater(RentEquipmentEntities db)
        {
            _db = db;
        }

        public string UpdateField()
        {
            var equipment = _db.Equipment.Find(EquipmentID);
            if (equipment == null)
            {
                throw new Exception($"Оборудование с ID {EquipmentID} не найдено.");
            }

            switch (FieldName.ToLower())
            {
                case "name":
                    equipment.Name = NewValue;
                    break;

                case "price":
                    if (decimal.TryParse(NewValue, out var price))
                    {
                        equipment.Price = price;
                    }
                    else
                    {
                        throw new Exception("Некорректное значение для поля Price.");
                    }
                    break;

                case "statusid":
                    if (int.TryParse(NewValue, out int statusId))
                    {
                        equipment.StatusID = statusId;
                    }
                    else
                    {
                        throw new Exception("Некорректное значение для поля StatusID.");
                    }
                    break;

                default:
                    throw new Exception($"Поле '{FieldName}' не поддерживается для обновления.");
            }

            _db.SaveChanges();

            return $"Поле '{FieldName}' успешно обновлено.";
        }
    }
}
