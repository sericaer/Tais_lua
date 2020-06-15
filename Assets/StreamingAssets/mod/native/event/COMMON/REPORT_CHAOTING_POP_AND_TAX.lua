
options = 
{
    OPTION_1 = 
    {
        report_pop_num = function ()
            return gm_data().chaoting.year_report_pop * 0.8
        end,

        desc = function (self)
            return {'REPORT_CHAOTING_POP_AND_TAX_OPTION_1_DESC', gm_data():tax_report(self.report_pop_num()), self.report_pop_num()}
        end,

        selected = function (self)
            local report_tax = gm_data():tax_report(self.report_pop_num())
            gm_data().economy = gm_data().economy - report_tax
            gm_data().chaoting.report_tax = report_tax
            gm_data().chaoting.year_report_pop = self.report_pop_num()
        end,

        tooltip = function (self)
            return {{'ECONOMY_CHANGED', gm_data():tax_report(self.report_pop_num())*-1}}
        end
    },

    OPTION_2 = 
    {
        report_pop_num = function ()
            return gm_data().chaoting.year_report_pop
        end,

        desc = function (self)
            return {'REPORT_CHAOTING_POP_AND_TAX_OPTION_2_DESC', gm_data():tax_report(self.report_pop_num()), self.report_pop_num()}
        end,

        selected = function (self)
            local report_tax = gm_data():tax_report(self.report_pop_num())
            gm_data().economy = gm_data().economy - report_tax
            gm_data().chaoting.report_tax = report_tax
            gm_data().chaoting.year_report_pop = self.report_pop_num()
        end,

        tooltip = function (self)
            return {{'ECONOMY_CHANGED', gm_data():tax_report(self.report_pop_num())*-1}}
        end
    },

    OPTION_3 = 
    {
        report_pop_num = function ()
            return gm_data().chaoting.year_report_pop * 1.2
        end,

        desc = function (self)
            return {'REPORT_CHAOTING_POP_AND_TAX_OPTION_3_DESC', gm_data():tax_report(self.report_pop_num()), self.report_pop_num()}
        end,

        selected = function (self)
            local report_tax = gm_data():tax_report(self.report_pop_num())
            gm_data().economy = gm_data().economy - report_tax
            gm_data().chaoting.report_tax = report_tax
            gm_data().chaoting.year_report_pop = self.report_pop_num()
        end,

        tooltip = function (self)
            return {{'ECONOMY_CHANGED', gm_data():tax_report(self.report_pop_num())*-1}}
        end
    },

    OPTION_4 = 
    {
        report_pop_num = function ()
            return gm_data().tax_pop_num
        end,

        desc = function (self)
            return {'REPORT_CHAOTING_POP_AND_TAX_OPTION_4_DESC', gm_data():tax_report(self.report_pop_num()), self.report_pop_num()}
        end,

        selected = function (self)
            local report_tax = gm_data():tax_report(self.report_pop_num())
            gm_data().economy = gm_data().economy - report_tax
            gm_data().chaoting.report_tax = report_tax
            gm_data().chaoting.year_report_pop = self.report_pop_num()
        end,

        tooltip = function (self)
            return {{'ECONOMY_CHANGED', gm_data():tax_report(self.report_pop_num())*-1}}
        end
    }
}
