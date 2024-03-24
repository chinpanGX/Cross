using Core.Model;
using Repository.SaveData;

namespace App.Title.Signup
{
    public class SignupModel : IModel
    {
        private readonly ISaveDataRepository repository;
        
        public SignupModel(ISaveDataRepository repository)
        {
            this.repository = repository;
        }
        
        public void Dispose()
        {
            // TODO マネージリソースをここで解放します
        }
        
        public void Execute()
        {
            
        }

        public void Register(string registerPlayerName)
        {
            repository.Register(registerPlayerName);
        }
    }
}