using Candidates_Project.Model;

namespace Candidates_Project.Repository
{
    public interface ICandidateRepo
    {
        public List<Candidate> GetAllCandidates();
        public Response GetCandidateById(int Id);
        public Response SaveCandidate(Candidate candidate);
        public Response UpdateCandidate(Candidate candidate);
        public Response DeleteCandidate(int Id);

    }
}
