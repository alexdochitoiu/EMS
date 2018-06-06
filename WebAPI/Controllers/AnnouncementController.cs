using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Data.Core.Domain.Entities;
using Data.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Infrastructure;
using WebAPI.Models.AnnouncementModels;

namespace WebAPI.Controllers
{
    [Route("api/announcements")]
    public class AnnouncementController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AnnouncementController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // GET api/announcements
        [HttpGet(Name = "AllAnnouncements")]
        [ProducesResponseType(typeof(List<DisplayAnnouncementModel>), 200)]
        //[AuthorizeToken]
        public async Task<ActionResult> AllAnnouncements()
        {
            try
            {
                var announcements = await _unitOfWork.Announcements
                    .GetAllAsync(
                        load: t => t.Include(a => a.User),
                        orderBy: t => t.Severity);
                var displayAnnouncements = _mapper.Map<List<DisplayAnnouncementModel>>(announcements);
                return Ok(displayAnnouncements);
            }
            catch (Exception exp)
            {
                Console.Write(exp);
                return BadRequest();
            }
        }

        // GET api/announcements/5
        [HttpGet("{id:guid}", Name = "AnnouncementById")]
        [ProducesResponseType(typeof(DisplayAnnouncementModel), 200)]
        public async Task<ActionResult> AnnouncementById(Guid id)
        {
            try
            {
                var announcement = await _unitOfWork.Announcements.GetByIdAsync(id);
                var displayAnnouncement = _mapper.Map<DisplayAnnouncementModel>(announcement);
                return Ok(displayAnnouncement);
            }
            catch (Exception exp)
            {
                Console.Write(exp);
                return BadRequest();
            }
        }
        
        // POST api/announcements
        [HttpPost(Name = "CreateAnnouncement")]
        [ProducesResponseType(typeof(DisplayAnnouncementModel), 201)]
        public async Task<ActionResult> CreateAnnouncement([FromBody]CreatingAnnouncementModel model)
        {
            try
            {
                var user = _unitOfWork.Users.GetByIdAsync(model.UserId);
                if (user == null) return BadRequest();

                var announcement = Announcement.Create(
                    model.Title,
                    model.Description,
                    model.Severity,
                    model.UserId);

                await _unitOfWork.Announcements.AddAsync(announcement);
                await _unitOfWork.CompleteAsync();

                var displayAnnouncement = _mapper.Map<DisplayAnnouncementModel>(announcement);
                return Ok(displayAnnouncement);
            }
            catch (Exception exp)
            {
                Console.Write(exp);
                return BadRequest();
            }
        }

        // PUT api/announcements/5
        [HttpPut("{id:guid}", Name = "UpdateAnnouncement")]
        [ProducesResponseType(typeof(DisplayAnnouncementModel), 200)]
        public async Task<ActionResult> UpdateAnnouncement(Guid id, [FromBody]CreatingAnnouncementModel model)
        {
            try
            {
                var announcement = await _unitOfWork.Announcements.GetByIdAsync(id);
                announcement.Update(
                    model.Title,
                    model.Description,
                    model.Severity,
                    model.UserId);

                await _unitOfWork.Announcements.EditAsync(announcement, id);
                await _unitOfWork.CompleteAsync();
                var displayAnnouncement = _mapper.Map<DisplayAnnouncementModel>(announcement);
                return Ok(displayAnnouncement);
            }
            catch (Exception exp)
            {
                Console.Write(exp);
                return BadRequest();
            }
        }

        // DELETE api/announcements/5
        [HttpDelete("{id:guid}", Name = "DeleteAnnouncement")]
        public ActionResult DeleteAnnouncement(Guid id)
        {
            try
            {
                var announcement = _unitOfWork.Announcements.GetByIdAsync(id).Result;
                _unitOfWork.Announcements.Delete(announcement);
                return _unitOfWork.CompleteAsync().Result != 1 ? 
                    BadRequest() : 
                    (ActionResult) Ok();
            }
            catch (Exception exp)
            {
                Console.Write(exp);
                return BadRequest();
            }
        }
    }
}