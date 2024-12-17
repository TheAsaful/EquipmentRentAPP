using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EquipmentRentAPP.Entities;
using EquipmentRentAPP.Models;

namespace EquipmentRentAPP.Controllers
{
    public class EquipmentController : ApiController
    {
        private RentEquipmentEntities db = new RentEquipmentEntities();

        // GET: api/Equipment
        [HttpGet]
        public IHttpActionResult GetEquipment()
        {
            try
            {
                var equipment = db.Equipment.ToList();
                return Ok(equipment);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, new { Message = $"Произошла ошибка: {ex.Message}" });
            }
        }

        // GET: api/Equipment/5
        [HttpGet]
        public IHttpActionResult GetEquipment(int id)
        {
            try
            {
                var equipmentItem = db.Equipment.Find(id);
                if (equipmentItem == null)
                {
                    return NotFound();
                }
                return Ok(equipmentItem);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, new { Message = $"Произошла ошибка: {ex.Message}" });
            }
        }

        // PUT: api/Equipment/5
        [HttpPut]
        public IHttpActionResult UpdateEquipment(EquipmentUpdater updater)
        {
            if (updater == null || string.IsNullOrWhiteSpace(updater.FieldName))
            {
                return BadRequest("Некорректные входные данные.");
            }

            try
            {
                var equipment = db.Equipment.Find(updater.EquipmentID);
                if (equipment == null)
                {
                    return NotFound();
                }

                switch (updater.FieldName.ToLower())
                {
                    case "name":
                        equipment.Name = updater.NewValue;
                        break;

                    case "price":
                        if (decimal.TryParse(updater.NewValue, out var price))
                        {
                            equipment.Price = price;
                        }
                        else
                        {
                            return BadRequest("Некорректное значение для Price.");
                        }
                        break;

                    case "statusid":
                        if (int.TryParse(updater.NewValue, out int statusId))
                        {
                            equipment.StatusID = statusId;
                        }
                        else
                        {
                            return BadRequest("Некорректное значение для StatusID.");
                        }
                        break;

                    default:
                        return BadRequest($"Поле '{updater.FieldName}' не поддерживается.");
                }

                db.SaveChanges();
                return Ok($"Поле '{updater.FieldName}' успешно обновлено.");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // POST: api/Equipment
        [HttpPost]
        public IHttpActionResult PostEquipment(Equipment equipment)
        {
            try
            {
                if (equipment == null)
                {
                    return BadRequest("Данные оборудования не переданы.");
                }

                db.Equipment.Add(equipment);
                db.SaveChanges();

                return Ok(new { Message = "Оборудование успешно добавлено.", EquipmentID = equipment.ID });
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, new { Message = $"Произошла ошибка: {ex.Message}" });
            }
        }

        // DELETE: api/Equipment/5
        [HttpDelete]
        public IHttpActionResult DeleteEquipment(int id)
        {
            try
            {
                var equipmentItem = db.Equipment.Find(id);
                if (equipmentItem == null)
                {
                    return NotFound();
                }

                // Изменяем статус оборудования на "Удалён"
                equipmentItem.StatusID = 3;  // Статус "Удалён" соответствует ID = 3
                db.SaveChanges();

                return Ok(new { Message = "Статус оборудования изменён на 'Удалён'." });
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, new { Message = $"Произошла ошибка: {ex.Message}" });
            }
        }

    }
}
