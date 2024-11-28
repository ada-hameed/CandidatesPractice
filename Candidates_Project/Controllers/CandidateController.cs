using Candidates_Project.Model;
using Candidates_Project.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Candidates_Project.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CandidateController : ControllerBase
    {
        private readonly ICandidateRepo candidateRepo;

        public CandidateController(ICandidateRepo candidateRepo)
        {
            this.candidateRepo = candidateRepo;
        }
        [HttpGet]
        public IActionResult GetAllCandidates()
        {
            var candidate = candidateRepo.GetAllCandidates();
            if (candidate.Count > 0)
            {

                return Ok(candidate);
            }
            return NoContent();


        }
        [HttpGet("{Id}")]
        public IActionResult GetCandidateById(int Id)
        {
            var candidate = candidateRepo.GetCandidateById(Id);
            return Ok(candidate);
        }
        [HttpPost]
        public IActionResult SaveCandidate(Candidate candidate)
        {
            var a = candidateRepo.SaveCandidate(candidate);
            return Ok(a);
        }
        [HttpPut]
        public IActionResult UpdateCandidate(Candidate candidate)
        {
            var a = candidateRepo.UpdateCandidate(candidate);
            return Ok(a);
        }
        [HttpDelete("{Id}")]
        public IActionResult DeleteCandidate(int Id)
        {
            var a = candidateRepo.DeleteCandidate(Id);
            return Ok(a);
        }

    }
}
