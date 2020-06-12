EVENT_DEF.EVENT_TEST = 
{
    trigger = function()
        return  gm_data().date.day % 3 == 0
    end,

    occur_days = function ()
        return 2
    end,

    options = 
    {
        OPTION_1 = 
        {
            selected = function ()
                print("EVENT_TEST".."_OPTION_1_DESC selected")
            end
        }
    }
}