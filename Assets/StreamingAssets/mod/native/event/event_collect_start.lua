EVENT_DEF.GLOBAL.EVENT_COLLECT_TAX_START = 
{
    occur_rate = function ()
        if gm_data().date.month == 1 and gm_data().date.day == 5 then
            return 1
        end
        return 0
    end,


    options = 
    {
        OPTION_1 = 
        {
            selected = function ()
                gm_data().tasks:find('COLLECT_TAX').start = true
                gm_data():collect_tax_start('level1')
            end
        }
    }
}