EVENT_DEF.GLOBAL.EVENT_CHAOTING_SCORE_TAISHOU = 
{
    occur_rate = function ()
        if gm_data().date.month == 12 and gm_data().date.day == 20 then
            return 1
        end
        return 0
    end,

    options = 
    {
        OPTION_1=
        {
            selected = function ()
            end,
        }
    }
}