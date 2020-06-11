EVENT_DEF.GLOBAL.EVENT_CHAOTING_TAX_IS_NOT_FULL = 
{
    occur_rate = function ()
        if gm_data().date.month == 12 and gm_data().date.day == 30 then
            if gm_data().chaoting.year_report_tax < gm_data().chaoting.year_expect_tax then
                return 0.1
            end
        end
        return 0
    end,

    options = 
    {
        OPTION_1 = 
        {
            selected = function ()
                gm_data().chaoting.prestige = gm_data().chaoting.prestige - 5
            end
        }
    }
}