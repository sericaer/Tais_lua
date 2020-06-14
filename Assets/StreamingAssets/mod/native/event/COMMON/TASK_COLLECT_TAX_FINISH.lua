desc = function ()
    return { 'TASK_COLLECT_TAX_FINISH_DESC', gm_data():tax_collect_expect() }
end

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
            return 'EVENT_REPORT_POP_AND_TAX_TO_CHAOTING'
        end 
    }
}