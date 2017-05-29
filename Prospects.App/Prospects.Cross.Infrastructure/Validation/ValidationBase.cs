
using GalaSoft.MvvmLight;

namespace Prospects.Cross.Infrastructure.Validation
{
    public abstract class ValidationBase : ViewModelBase
    {
        protected ValidationBase()
        {
            ValidationErrors = new ValidationErrors();            
        }

        private ValidationErrors validationErrors;

        public ValidationErrors ValidationErrors
        {
            get
            {
                return validationErrors;
            }
            set
            {
                Set<ValidationErrors>(ref validationErrors, value);
            }
        }
        

        public bool IsValid { get; private set; }

        public void Validate()
        {
            ValidationErrors.Clear();
            ValidateSelf();
            IsValid = ValidationErrors.IsValid;

            RaisePropertyChanged("IsValid");

            RaisePropertyChanged("ValidationErrors");
        }

        protected abstract void ValidateSelf();
    }
}