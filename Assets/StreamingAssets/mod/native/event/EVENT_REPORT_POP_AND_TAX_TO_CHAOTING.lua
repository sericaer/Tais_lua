EVENT_DEF.GLOBAL.EVENT_REPORT_POP_AND_TAX_TO_CHAOTING = 
{
    options = 
    {
        OPTION_1 = 
        {
            desc = function (self)
                return self.report_pop_num()
            end,

            report_pop_num = function ()
                return gm_data().chaoting.reg_pop_num * 0.008
            end,

            selected = function (self)
                local report_tax = gm_data():report_tax(self.report_pop_num())
                gm_data().economy = gm_data().economy - report_tax
                gm_data().chaoting.report_tax = report_tax
                gm_data().chaoting.year_report_pop = self.report_pop_num()
            end
        },

        OPTION_2 = 
        {
            desc = function (self)
                return self.report_pop_num()
            end,

            report_pop_num = function ()
                return gm_data().chaoting.reg_pop_num
            end,

            selected = function (self)
                local report_tax = gm_data():report_tax(self.report_pop_num())
                gm_data().economy = gm_data().economy - report_tax
                gm_data().chaoting.report_tax = report_tax
                gm_data().chaoting.year_report_pop = self.report_pop_num()
            end
        },

        OPTION_3 = 
        {
            report_pop_num = function ()
                return gm_data().chaoting.reg_pop_num * 1.2
            end,

            desc = function (self)
                return self.report_pop_num()
            end,

            selected = function (self)
                local report_tax = gm_data():report_tax(self.report_pop_num())
                gm_data().economy = gm_data().economy - report_tax
                gm_data().chaoting.report_tax = report_tax
                gm_data().chaoting.year_report_pop = self.report_pop_num()
            end
        },

        OPTION_4 = 
        {
            report_pop_num = function ()
                return gm_data().tax_pop_num
            end,

            desc = function (self)
                return self.report_pop_num()
            end,

            selected = function (self)
                local report_tax = gm_data():report_tax(self.report_pop_num())
                gm_data().economy = gm_data().economy - report_tax
                gm_data().chaoting.report_tax = report_tax
                gm_data().chaoting.year_report_pop = self.report_pop_num()
            end
        }
    }
}