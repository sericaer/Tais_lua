options = 
{
    OPTION_1 = 
    {
        selected = function ()
            gm_data().economy = gm_data().economy  + gm_data():tax_collect_finish()
        end,

        tooltip = function ()
            return { {'ECONOMY_CHANGED', gm_data():tax_collect_expect()} }
        end,

        next_event = function ()
            return 'REPORT_CHAOTING_POP_AND_TAX'
        end 
    }
}