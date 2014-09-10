﻿using MixERP.Net.Common.Models.Core;
using MixERP.Net.WebControls.Common.Resources;

/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This file is part of MixERP.

MixERP is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

MixERP is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with MixERP.  If not, see <http://www.gnu.org/licenses/>.
***********************************************************************************/

using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MixERP.Net.WebControls.Common
{
    [ToolboxData("<{0}:DateTextBox runat=server></{0}:DateTextBox>")]
    public partial class DateTextBox //: CompositeControl
    {
        //Todo: Display localized calendar.
        private TextBox textBox;

        private CompareValidator compareValidator;
        private RequiredFieldValidator requiredValidator;

        protected override void Render(HtmlTextWriter w)
        {
            this.textBox.RenderControl(w);

            if (this.EnableValidation)
            {
                this.compareValidator.RenderControl(w);
            }
        }

        protected override void RecreateChildControls()
        {
            this.EnsureChildControls();
        }

        private void InitializeDate(Frequency frequency)
        {
            //Todo:Fix this implementation.
            DateTime date = DateTime.Today;

            if (frequency == Frequency.MonthStartDate)
            {
                date = date.AddDays(1 - date.Day);
            }

            if (frequency == Frequency.MonthEndDate)
            {
                date = new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month));
            }

            if (this.textBox != null)
            {
                this.textBox.Text = date.ToShortDateString();
            }
        }

        protected override void CreateChildControls()
        {
            this.Controls.Clear();

            this.textBox = new TextBox();
            this.textBox.ID = this.ID;

            this.compareValidator = new CompareValidator();
            this.compareValidator.Display = ValidatorDisplay.Dynamic;

            this.compareValidator.ID = this.ID + "CompareValidator";
            this.compareValidator.ControlToValidate = this.ID;
            this.compareValidator.ValueToCompare = "1/1/1900";
            this.compareValidator.Type = ValidationDataType.Date;

            this.compareValidator.ErrorMessage = CommonResource.InvalidDate;
            this.compareValidator.EnableClientScript = true;
            this.compareValidator.CssClass = this.ValidatorCssClass;

            this.requiredValidator = new RequiredFieldValidator();
            this.requiredValidator.Display = ValidatorDisplay.Dynamic;

            this.requiredValidator.ID = this.ID + "RequiredFieldValidator";
            this.requiredValidator.ControlToValidate = this.ID;
            this.requiredValidator.ErrorMessage = CommonResource.RequiredField;
            this.requiredValidator.EnableClientScript = true;
            this.requiredValidator.CssClass = this.ValidatorCssClass;

            this.Controls.Add(this.textBox);

            if (this.EnableValidation)
            {
                this.Controls.Add(this.compareValidator);
            }

            if (this.Required)
            {
                this.Controls.Add(this.requiredValidator);
            }

            this.AddjQueryUiDatePicker();
        }
    }
}