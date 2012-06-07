namespace DevBridge.Templates.WebProject.DataContracts
{ 
    public interface IUnitOfWorkRepository
    {        
        void Use(IUnitOfWork unitOfWork);
    }
}
