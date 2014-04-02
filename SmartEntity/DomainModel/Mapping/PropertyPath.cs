namespace HeptaSoft.SmartEntity.DomainModel.Mapping
{
    public class PropertyPath
    {
        public const string Separator = ">";

        private readonly PropertyPath parentPath;

        public string PropertyName { get; private set; }

        public PropertyPath(string propertyName)
        {
            this.PropertyName = propertyName;
        }

        public PropertyPath(PropertyPath parentPath, string propertyName)
        {
            this.parentPath = parentPath;
            this.PropertyName = propertyName;
        }

        public string AbsolutePath
        {
            get
            {
                if (this.parentPath != null)
                {
                    return (this.parentPath.AbsolutePath + Separator + this.PropertyName);
                }
                else
                {
                    return this.PropertyName;
                }
            }
        }

    }
}
