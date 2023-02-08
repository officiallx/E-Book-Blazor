using ebook.Models.General;
using ebook.Services.Interface;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace ebook.Shared.Component
{
    public partial class DropDown
    {
        [Parameter]
        public string TableName { get; set; }
        [Parameter]
        public string LabelName { get; set; }
        
        [Parameter]
        public string InitialValue { get; set; }
        [Parameter]
        public bool All { get; set; } = false;
        [Parameter]
        public bool Select { get; set; } = false;
        [Parameter]
        public bool Disabled { get; set; } = false;
        [Parameter]
        public Margin Margin { get; set; }
        [Parameter]
        public Variant Variant { get; set; }

        [Inject] private ICommonService _commonservice { get; set; }

        private List<DropDownModel> _items = new();

        private string _value;

        public string BindingValue
        {
            get => _value;
            set
            {
                if (_value == value) return;
                _value = value;
                ValueChanged.InvokeAsync(value);
            }
        }

        [Parameter]
        public EventCallback<string> ValueChanged { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await LoadDropDown();
            if (string.IsNullOrEmpty(LabelName))
            {
                LabelName = TableName;

            }

        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await LoadDropDown();

            if (!string.IsNullOrEmpty(InitialValue))
            {
                BindingValue = InitialValue;
            }
            //else
            //{
            //    BindingValue = "";
            //}
        }


        private async Task LoadDropDown()
        {
            var response = await _commonservice.GetDropDownAsync(TableName);

            _items = response;

            if (All)
            {
                _items.Insert(0, new DropDownModel() { Id = -1,Value = "ALL" });
            }
            if (Select)
            {
                _items.Insert(0, new DropDownModel() { Id = 0,Value = "SELECT" });
            }

        }
        private async Task<IEnumerable<string>> SearchItems(string value)
        {
            if (string.IsNullOrEmpty(value))
                return _items.Select(x => x.Id.ToString());

            return _items.Where(x => x.Value.Contains(value, StringComparison.InvariantCultureIgnoreCase))
                .Select(x => x.Id.ToString());
        }
    }
}