EVENT_DEF.GLOBAL.EVENT_TASK_COLLECT_TAX_FINISH = 
{
    options = 
    {
        OPTION_1 = 
        {
            desc = function ()
                return gm_data():tax_collect_expect()
            end,

            selected = function ()
                gm_data().economy = gm_data().economy  + gm_data():tax_collect_finish()
            end,

            next_event = function ()
                return 'EVENT_REPORT_POP_AND_TAX_TO_CHAOTING'
            end 
        }
    }
}