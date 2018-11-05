namespace Contracts.DbModels
{
    public abstract class BaseModel<T>
    {
        //Initiates the id of the derived classes with the T type
        public T Id { get; set; }
    }
}
