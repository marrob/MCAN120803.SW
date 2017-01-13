
namespace Konvolucio.MCAN120803.GUI
{
    using DataStorage;
    using Services;

    class StorageAppEvent : IApplicationEvent
    {

        public FileChangingType ChangingType { get; private set; }
        public Storage Storage { get; private set; }
        public StorageChanegdEventArgs Details { get; private set; }
        /// <summary>
        /// A project akcióit követi.
        /// </summary>
        public StorageAppEvent(Storage storage, FileChangingType changingType)
        {
            ChangingType = changingType;
            Storage = storage;
            Details = new StorageChanegdEventArgs(DataObjects.Unknown);
        }

        /// <summary>
        /// A project akcióit követi. és részletezi amnyira tudja...
        /// 
        /// A Details segitségével beazonisítható, hogy konkrétan mi változott a projectben.
        /// Segítségével célzottan módosíthatsz.
        /// Pl:
        /// 
        /// EventAggregator.Instance.Subscribe<ProjectFileAppEvent/>(e =>
        /// {
        ///     if (e.ChangingType == FileChangingType.LoadComplete ||
        ///         e.ChangingType == FileChangingType.ContentChanged)
        ///     {
        ///         if (e.Details.ProjectObjects == ProjectObjects.ParameterProperty &&
        ///             e.Details.PropertyDescriptor.Name == PropertyPlus.GetPropertyName(() => e.Project.Parameters.Termination))
        ///         {
        ///             _parmeters = e.Project.Parameters;
        ///             Checked = _parmeters.Termination;
        ///         }
        /// 
        ///     }
        /// });
        /// </summary>
        public StorageAppEvent(Storage storage, FileChangingType changingType, StorageChanegdEventArgs args)
        {
            ChangingType = changingType;
            Storage = storage;
            Details = args;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string str = string.Empty;
            str += this.GetType().Name + ", ";
            str += "Name: " + (Storage != null ? Storage.FileName : "UnKnown") + ", ";
            str += "Event: " + ChangingType + ".";
            return str;
        }
    }
}
