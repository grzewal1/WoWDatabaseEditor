﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WDE.SmartScriptEditor.Data;
using WDE.SmartScriptEditor.Editor.ViewModels;
using WDE.SmartScriptEditor.Editor.Views;
using Prism.Ioc;
using WDE.Module.Attributes;

namespace WDE.SmartScriptEditor.Data
{
    [AutoRegister]
    public class SmartTypeListProvider : ISmartTypeListProvider
    {
        private readonly ISmartDataManager smartDataManager;

        public SmartTypeListProvider(ISmartDataManager smartDataManager)
        {
            this.smartDataManager = smartDataManager;
        }

        public int? Get(SmartType type, Func<SmartGenericJsonData, bool> predicate)
        {
            var view = new SmartSelectView();
            var model = new SmartSelectViewModel(GetFileNameFor(type), type, predicate, smartDataManager);
            view.DataContext = model;

            bool? res = view.ShowDialog();

            if (res.HasValue && res.Value)
                return model.SelectedItem.Id;

            return null;
        }

        private string GetFileNameFor(SmartType type)
        {
            switch (type)
            {
                case SmartType.SmartEvent:
                    return "events.txt";
                case SmartType.SmartAction:
                    return "actions.txt";
                case SmartType.SmartTarget:
                    return "targets.txt";
                case SmartType.SmartSource:
                    return "targets.txt";
            }
            return null;
        }
    }
}
