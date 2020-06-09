EVENT_DEF.DEPART.EVENT_DEPART_HARVEST_NORMAL = 
{
    hide = true,
    
    options = 
    {
        OPTION_1 = 
        {
            selected = function ()
                gm_depart().buffers:set_valid('HARVEST_NORMAL')
            end,
        }
    }
}