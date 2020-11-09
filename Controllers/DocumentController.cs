using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PopApp.Core.Dtos;
using PopApp.Core.Entities;
using PopApp.Core.Interfaces.Services;

namespace PopAppMaster.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private readonly IDocumentsServices _repo;
        private readonly IMapper _mapper;
        public DocumentController(IDocumentsServices repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetDocuments()
        {
            try
            {
                var documents = await _repo.GetDocuments();
                var documentsDto = _mapper.Map<IEnumerable<DocumentDto>>(documents);
                return Ok(documentsDto);
            }
            catch (Exception e)
            {
                return Ok(e.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDocument(int id)
        {
            try
            {
                var document = await _repo.GetDocument(id);
                var documentDto = _mapper.Map<DocumentDto>(document);
                return Ok(documentDto);

            }
            catch (Exception e)
            {
                return Ok(e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostDocument(IFormFile files)
        {
            try
            {

                DocumentDto documentDto = new DocumentDto
                {
                    DocumentTitle = files.FileName,
                    CreateAt = DateTime.Now

                };

                using (var target = new MemoryStream())
                {
                    files.CopyTo(target);
                    documentDto.DocumentImage = target.ToArray();
                }

                var document = _mapper.Map<Document>(documentDto);
                await _repo.CreateDocument(document);
                return Ok("document was Registred");
            }
            catch (Exception e)
            {
                return Ok(e.Message);
            }
        }
    }
}
