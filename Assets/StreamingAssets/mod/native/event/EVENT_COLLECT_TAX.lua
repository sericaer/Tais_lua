EVENT_DEF.GLOBAL.EVENT_COLLECT_TAX_START = 
{
    occur_rate = function ()
        if gm_data().date.month == 9 and gm_data().date.day == 1 then
            return 1
        end
        return 0
    end,


    options = 
    {
        OPTION_1 = 
        {
            selected = function ()
                local task = gm_data().tasks:find('COLLECT_TAX')
                task.level = 1
                task.start = true
            end
        }
    }
}