EVENT_DEF.GLOBAL.EVENT_TASK_COLLECT_TAX_FINISH = 
{
    options = 
    {
        OPTION_1 = 
        {
            desc = function ()
                return gm_data().tax_expect.value
            end,

            selected = function ()
                gm_data().tax_expect:finish();
            end
        }
    }
}