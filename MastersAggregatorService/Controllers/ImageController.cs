using MastersAggregatorService.Models;
using MastersAggregatorService.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace MastersAggregatorService.Controllers;
[Route("[controller]")] 
public class ImageController : BaseController<Image>
{ 
    private ImageRepository _imageRepository { get; set; }

    //public ImageController(BaseRepository<Image> repository) : base(repository)
    public ImageController(ImageRepository repository) : base(repository)
    {
        _imageRepository = repository;
    }

    // GET all image
    [HttpGet] 
    public JsonResult GetAll()
    { 
        return new JsonResult(_imageRepository.GetAll());
    }
    // GET id image
    [HttpGet("{id}")]
    public JsonResult GetById(int id)
    {  
        if (_imageRepository.GetById(id) == null)
            return new JsonResult("Image does not exist");

        return new JsonResult(_imageRepository.GetById(id));  
    }


}